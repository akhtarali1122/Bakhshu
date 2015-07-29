using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using ShopOnline.Models;
using ShopOnline.Filters;
using System.Web.Routing;
using System.IO;
using System.Drawing;

namespace ShopOnline.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        protected string SmtpUserName
        {
            get { return string.IsNullOrEmpty(Constants.SMTP_USER_NAME_KEY) ? string.Empty : Constants.SMTP_USER_NAME_KEY; }
        }
        //[SessionExpireFilter]
        public ActionResult Index()
        {
            ViewBag.TitleDes = "Free shop advertisement";
            if (new UserSession().isUserLogin() == false)
            {
                Session["CaptchaCode"] = Utils.getCaptchaCode();
                return View();
            }
            else
            {
                return RedirectToRoute(new RouteValueDictionary(new { controller = "Home", action = "ShopIndex" }));
            }
        }
        
        [SessionExpireFilter]
        public ActionResult ShopRegister()
        {
            ViewBag.TitleDes = "Free shop advertisement";
            if (new UserSession().EverLogin_User() == false)
            {
                return View();
            }
            else
            {
                Int64 userid = new UserSession().getUserSession().UserId;
                User user=new User().GetUser(userid);
                ViewData.Model = user;
                return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = user.UserId }));
            }
        }
        //[SessionExpireFilter]
        //public ActionResult UpdateShop()
        //{
        //    ViewBag.TitleDes = "Free shop advertisement";
        //    Int64 userid = new UserSession().getUserSession().UserId;
        //    User user = new User().GetUser(userid);
        //    if (new UserSession().EverLogin_User() == true)
        //    {
        //        ViewData["txtShopName"] = user.ShopName;
        //        ViewData["txtShopCountry"] = user.ShopCountry;
        //        ViewData["txtShopCity"] = user.ShopCity;
        //        ViewData["txtShopAddress"] = user.ShopAddress;
        //        ViewData["txtMapUrl"] = user.ShopMapUrl;
        //        ViewData["txtShopDescription"] = user.ShopDescription;
        //    }

        //    return View("ShopRegister");
        //}
        
        
        [HttpPost]
        public ActionResult RegisterUser(string txtCaptchaCode, string txtLoginName, string txtEmail, string txtPassword, string txtConfirmPassword, string txtFirstName, string txtLastName, string txtContactNumber, string radGender, string txtDay, string txtMonth, string txtYear)
        {
            try
            {
                ViewBag.TitleDes = "Free shop advertisement";
                Boolean validInput = true;
                ViewData["txtLoginName"] = txtLoginName;
                ViewData["txtPassword"] = txtPassword;
                ViewData["txtConfirmPassword"] = txtConfirmPassword;
                ViewData["txtFirstName"] = txtFirstName;
                ViewData["txtLastName"] = txtLastName;
                ViewData["txtContactNumber"] = txtContactNumber;
                ViewData["txtEmail"] = txtEmail;

                ViewData["radGender"] = radGender;
                ViewData["txtDay"] = txtDay;
                ViewData["txtMonth"] = txtMonth;
                ViewData["txtYear"] = txtYear;


                if (string.IsNullOrWhiteSpace(txtCaptchaCode))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter security code");
                }

                if (string.IsNullOrWhiteSpace(txtLoginName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter login name");
                }
                else
                {
                    if (new User().CheckUserAvailablity(txtLoginName.Trim()) == false)
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Sorry Login Name Not Available", "Sorry, Login name not available. Please try with different login name.");
                    }
                }


                if (string.IsNullOrWhiteSpace(txtEmail))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter email");
                }

                if (string.IsNullOrWhiteSpace(txtPassword))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter password");
                }
                if (string.IsNullOrWhiteSpace(txtConfirmPassword))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please confirm your password");
                }
                if (string.IsNullOrWhiteSpace(txtFirstName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter first name");
                }
                if (string.IsNullOrWhiteSpace(txtLastName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter last name");
                }
                //my own
                if (txtDay.Equals("0"))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please select day of birth");
                }
                if (txtMonth.Equals("0"))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please select month of birth");
                }
                if (txtYear.Equals("0"))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please select year of birth");
                }
                if (string.IsNullOrWhiteSpace(radGender))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please select gender");
                }
                //limit

                if (!string.IsNullOrWhiteSpace(txtPassword) && !string.IsNullOrWhiteSpace(txtConfirmPassword))
                {
                    if (!txtPassword.Trim().Equals(txtConfirmPassword.Trim()))
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Incorrect password", "Password and confirm password does not match.");
                    }
                }


                if (!string.IsNullOrWhiteSpace(txtCaptchaCode))
                {
                    if (!txtCaptchaCode.Trim().Equals(new SecurityManager().DecodeString(Session["CaptchaCode"].ToString())))
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Invalid security code", "Please enter valid security code");
                    }
                }


                if (validInput == false)
                {
                    Session["CaptchaCode"] = Utils.getCaptchaCode();
                    return View("Index");
                }
                else
                {
                    User userObj = new User();
                    userObj.UserRoleId = Convert.ToInt32(UserRolesType.RegisteredUser);
                    userObj.UserFirstName = txtFirstName;
                    userObj.UserLastName = txtLastName;
                    userObj.UserLoginName = txtLoginName;
                    userObj.Contact_Email = txtEmail;
                    userObj.UserPassword = txtPassword.Trim();
                    userObj.UserContactNumber = txtContactNumber;
                    
                    userObj.Gender = radGender;
                    userObj.DOB = Convert.ToDateTime(txtDay + "/" + txtMonth + "/" + txtYear).Date;
                    userObj.Is_ShopCreated= false;
                    userObj.Created_On = DateTime.UtcNow;
                    userObj.Last_Updated_On = DateTime.UtcNow;
                    userObj.UserStatus = Convert.ToInt16(UserStatusType.InActive);

                    userObj.ProfilePhoto = "BlankImage.jpg";

                    long UserId = new User().CreateUser(userObj);
                    if (UserId <= 0)
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Error", "Error occured during process Please try again");
                    }
                    else
                    {

                        if (UserId > 0)
                        {
                            string UserIdEncrypted = new SecurityManager().EncodeString(UserId.ToString());
                            string linkUrl = Utils.getSiteUrl() + "/Register/ActivateMe?mySite=" + UserIdEncrypted;

                            string emailMessage = "<div><b>Your User account created successfully</b></div>";
                            emailMessage = emailMessage + "<div style='padding-top:10px'>Your account is not active for the time being. Click the link below to activate your account</div>";
                            emailMessage = emailMessage + "<div><a href='" + linkUrl + "'>" + linkUrl + "</a></div>";

                            string emailHtml = Utils.GetEmailTemplate(txtFirstName + " " + txtLastName, emailMessage);

                            new EmailManager().SendEmail(txtEmail, SmtpUserName, "Account Activation Link", emailHtml);

                            return RedirectToRoute(new RouteValueDictionary(new { controller = "Register", action = "UserRegistered", webId = UserIdEncrypted }));
                        }
                        else
                        {
                            validInput = false;
                            ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Error", "Error occured during process Please try again");
                        }

                    }
                }
            }
            catch (Exception)
            {
            }
            Session["CaptchaCode"] = Utils.getCaptchaCode();
            return View("Index");
        }

        [HttpPost][ValidateInput(false)]
        public ActionResult RegisterShop(string txtShopName, string txtShopCountry, string txtShopCity, string txtShopAddress, string txtMapUrl, string txtShopDescription)
        {
            try
            {
                Boolean validInput = true;

                ViewData["txtShopName"] = txtShopName;
                ViewData["txtShopCountry"] = txtShopCountry;
                ViewData["txtShopCity"] = txtShopCity;
                ViewData["txtShopAddress"] = txtShopAddress;
                ViewData["txtMapUrl"] = txtMapUrl;
                ViewData["txtShopDescription"] = txtShopDescription;

                
                //my own
                if (string.IsNullOrWhiteSpace(txtShopName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter shop name");
                }
                if (string.IsNullOrWhiteSpace(txtShopCountry))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter shop country name");
                }
                if (string.IsNullOrWhiteSpace(txtShopCity))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter shop city name");
                }
                if (string.IsNullOrWhiteSpace(txtShopAddress))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter shop address");
                }
                if (string.IsNullOrWhiteSpace(txtShopDescription))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter shop description");
                }
                
                //limit

                
                if (validInput == true)
                {

                    Int64 userid = new UserSession().getUserSession().UserId;

                    User userObj = new User();
                    userObj.UserId = userid;
                    userObj.ShopName = txtShopName;
                    userObj.ShopCountry = txtShopCountry;
                    userObj.ShopCity = txtShopCity;
                    userObj.ShopAddress = txtShopAddress;
                    userObj.ShopMapUrl = txtMapUrl;
                    userObj.ShopDescription = txtShopDescription;
                    userObj.Shop_Last_Updated_On = DateTime.UtcNow;
                    userObj.Is_ShopCreated= true;
                    User retShop = new User().CreateShop(userObj);
                    if (retShop==null)
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Error", "Error occured during process Please try again");
                    }
                    else
                    {
                        string USER_ID = new ShopOnline.Common.SecurityManager().EncodeString(Convert.ToString(retShop.UserId));
                        new UserSession().SetUserSession(retShop);
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = USER_ID }));
                    }
                }
            }
            catch (Exception)
            {
            }
            
            return View("ShopRegister");
        }

        public ActionResult CheckUserAvailability(string txtLoginName)
        {

            try
            {

                if (!string.IsNullOrEmpty(txtLoginName))
                {
                    Boolean retResult = new User().CheckUserAvailablity(txtLoginName.Trim());
                    if (retResult == true)
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        [ValidateInput(false)]
        public ActionResult UserRegistered(string webId)
        {
            try
            {
                if (!string.IsNullOrEmpty(webId))
                {
                    ViewData.Model = new User().GetUser(Convert.ToInt64(new SecurityManager().DecodeString(webId)));
                }
            }
            catch (Exception)
            {
            }
            return View("UserRegistrationSuccess");
        }
        [ValidateInput(false)]
        public ActionResult ActivateMe(string mySite)
        {
            try
            {
                if (!string.IsNullOrEmpty(mySite))
                {
                    string UserId = new SecurityManager().DecodeString(mySite);
                    Boolean retResult = new User().ChangeUserStatus(Convert.ToInt64(UserId), Convert.ToInt16(UserStatusType.Active));
                    if (retResult == true)
                    {
                        ViewData.Model = new User().GetUser(Convert.ToInt64(UserId));

                    }

                }
            }
            catch (Exception)
            {

            }
            return View("UserActivation");
        }

        #region rough code
        //[sessionexpirefilter]
        //[shopcreatedfilter]
        //public actionresult uploadprofileimages()
        //{
        //    try
        //    {
        //        int64 userid = new usersession().getusersession().userid;
        //        viewdata.model = new user().getuser(userid);
        //    }
        //    catch (exception)
        //    { }
        //    return view("uploadprofilephoto");
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult ChangeProfileAvatar(HttpPostedFileBase fileUserProfile, string UserId)
        //{

        //    try
        //    {
        //        //Int64 userid = new UserSession().getUserSession().UserId;
        //        User Old_userObj = new User().GetUser(Convert.ToInt64(UserId));
        //        string savedFileName = "";
        //        if (fileUserProfile != null)
        //        {
        //            savedFileName = uploadProfileImage(fileUserProfile);
        //            if (!string.IsNullOrWhiteSpace(savedFileName))
        //            {
        //               string filename = new User().ChangeProfilePhoto(Old_userObj.UserId, savedFileName);
        //                //New_userObj.ProfilePhoto = savedFileName;
        //               deleteUploadedImage(filename);
        //                User retObj = new User().AuthnticateUser(Old_userObj);
        //                if (retObj != null)
        //                {
        //                    new UserSession().SetUserSession(retObj);
        //                }


        //            }
        //        }
        //        Old_userObj = null;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return RedirectToRoute(new RouteValueDictionary(new { controller = "Register", action = "UploadProfileImages" }));
        //}
        #endregion
        #region uploading
        
        //public string uploadProfileImage(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        string fileName = "";
        //        string retFileName = "";
        //        if (file != null)
        //        {
        //            fileName = new UserSession().getUserSession().UserId + "_" + DateTime.UtcNow.ToString();
        //            fileName = fileName.Replace(" ", "");
        //            fileName = fileName.Replace("/", "_");
        //            fileName = fileName.Replace(":", "_");
        //            fileName = fileName + Path.GetFileName(file.FileName);
        //            retFileName = fileName;

        //            string userFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES);
        //            if (!Directory.Exists(userFolder))
        //            {
        //                Directory.CreateDirectory(userFolder);
        //            }

        //            string filePath = userFolder + "\\" + fileName;
        //            file.SaveAs(filePath);
                    
                    
        //            file.InputStream.Flush();
        //            file.InputStream.Close();
        //            file.InputStream.Dispose(); 
                    
        //            GenerateThumbNail(retFileName);
        //        }
        //        return retFileName;
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}
        //public void deleteUploadedImage(string imagePath)
        //{
        //    try
        //      {
        //        string filePathName = Constants.UPLOADED_IMAGES + "/" + imagePath;
        //        FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
        //        if (TheFile.Exists)
        //        {
        //            System.IO.File.Delete(Server.MapPath("../" + filePathName));
        //        }

        //        filePathName = Constants.UPLOADED_IMAGES_THUMBNAILS + "/" + imagePath;
        //        TheFile = new FileInfo(Server.MapPath("../" + filePathName));
        //        if (TheFile.Exists)
        //        {
        //            System.IO.File.Delete(Server.MapPath("../" + filePathName));
        //        }

        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //private void GenerateThumbNail(String imageName)
        //{
        //    try
        //    {
        //        string userFolder = Constants.UPLOADED_IMAGES;
        //        string filePathName = userFolder + "/" + imageName;
        //        FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
        //        if (TheFile.Exists)
        //        {
        //            System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(Server.MapPath("../" + filePathName));
        //            Size thumbnailSize = GetThumbnailSize(uploadedImage);
        //            System.Drawing.Image thumbNailImage = uploadedImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
        //            uploadedImage.Dispose();
                    
        //            String thumbnailFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_THUMBNAILS);
        //            if (!Directory.Exists(thumbnailFolder))
        //            {
        //                Directory.CreateDirectory(thumbnailFolder);
        //            }

        //            String filePath = thumbnailFolder + "\\" + imageName;


        //            thumbNailImage.Save(filePath);
        //            thumbNailImage.Dispose();
                    
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //static Size GetThumbnailSize(Image original)
        //{
        //    // Maximum size of any dimension.
        //    const int maxPixels = Constants.THUMBNAIL_WIDTH;

        //    // Width and height.
        //    int originalWidth = original.Width;
        //    int originalHeight = original.Height;

        //    // Compute best factor to scale entire image based on larger dimension.
        //    double factor;
        //    if (originalWidth > originalHeight)
        //    {
        //        factor = (double)maxPixels / originalWidth;
        //    }
        //    else
        //    {
        //        factor = (double)maxPixels / originalHeight;
        //    }

        //    // Return thumbnail size.
        //    return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        //}

        #endregion


    }
}
