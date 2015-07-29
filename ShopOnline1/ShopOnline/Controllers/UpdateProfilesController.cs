using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;
using ShopOnline.Filters;
using ShopOnline.Common;
using System.Web.Routing;
using System.IO;
using System.Drawing;

namespace ShopOnline.Controllers
{
    [SessionExpireFilter]
    public class UpdateProfilesController : Controller
    {
        //
        // GET: /UpdateProfiles/

        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult CheckUserAvailability(string txtLoginName)
        {

            try
            {

                if (!string.IsNullOrEmpty(txtLoginName))
                {
                    string loginName = new UserSession().getUserSession().UserLoginName;

                    Boolean retResult = new User().CheckUserAvailablity(txtLoginName.Trim());
                    if (loginName == txtLoginName)
                    {
                        Response.Write("2");
                    }
                    else
                    {
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
                else
                {
                    Response.Write("2");
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        #region Update User
        public ActionResult UpdateUser(string id)
        {

            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_PROFILE;
            ViewData[Constants.MENU] = Constants.EDIT_USER_PROFILE;

            ViewBag.TitleDes = "Free shop advertisement";
            Int64 userid = new UserSession().getUserSession().UserId;
            User user = new User().GetUser(userid);
            ViewData.Model = user;

            //ViewData["txtLoginName"] = user.UserLoginName;
            //ViewData["txtEmail"] = user.Contact_Email;
            //ViewData["txtFirstName"] = user.UserFirstName;
            //ViewData["txtLastName"] = user.UserLastName;
            //ViewData[" txtContactNumber"] = user.UserContactNumber;
            //ViewData["radGender"] = user.Gender;
            //string s = Convert.ToString(user.DOB);
            //string[] values = s.Split('/', ' ');
            //int ii = values.Length;
            //for (int i = 0; i < values.Length; i++)
            //{
            //    values[i] = values[i].Trim();
            //    if (i == 0)
            //    {
            //        string day = values[i].Trim();
            //        ViewData["txtDay"] = values[i].Trim();
            //    }
            //    else if (i == 1)
            //    {
            //        string month = values[i].Trim();
            //        ViewData["txtMonth"] = values[i].Trim();
            //    }
            //    else if (i == 2)
            //    {
            //        string year = values[i].Trim();
            //        ViewData["txtYear"] = values[i].Trim();
            //    }
            //}
            return View();
        }
        [HttpPost]
        public ActionResult UpdateUser(string txtLoginName, string txtFirstName, string txtLastName, string txtCellNumber, string txtEmail, string radGender, string txtDay, string txtMonth, string txtYear, string userId)
        {
            try
            {
                Boolean validInput = true;
                ViewData["txtLoginName"] = txtLoginName;
                ViewData["txtFirstName"] = txtFirstName;
                ViewData["txtLastName"] = txtLastName;
                ViewData["txtContactNumber"] = txtCellNumber;
                ViewData["txtEmail"] = txtEmail;

                ViewData["radGender"] = radGender;
                ViewData["txtDay"] = txtDay;
                ViewData["txtMonth"] = txtMonth;
                ViewData["txtYear"] = txtYear;


                if (string.IsNullOrWhiteSpace(txtLoginName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter login name");
                }
                else
                {
                    //if (new User().CheckUserAvailablity(txtLoginName.Trim()) == false)
                    //{
                    //    validInput = false;
                    //    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Sorry Login Name Not Available", "Sorry, Login name not available. Please try with different login name.");
                    //}
                }


                if (string.IsNullOrWhiteSpace(txtEmail))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter email");
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

                if (validInput == true)
                {
                    User userObj = new User();
                    //userObj.UserRoleId = Convert.ToInt32(UserRolesType.RegisteredUser);
                    userObj.UserId =Convert.ToInt64(userId);
                    userObj.UserFirstName = txtFirstName;
                    userObj.UserLastName = txtLastName;
                    userObj.UserLoginName = txtLoginName;
                    userObj.Contact_Email = txtEmail;
                    userObj.Gender = radGender;
                    userObj.DOB = Convert.ToDateTime(txtDay + "/" + txtMonth + "/" + txtYear).Date;
                    userObj.Last_Updated_On = DateTime.UtcNow;

                    User user = new User().UpdateUser(userObj);
                    if (user == null)
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Error", "Error occured during process Please try again");
                    }
                    else
                    {

                        return RedirectToRoute(new RouteValueDictionary(new { controller = "UpdateProfiles", action = "UpdateUser", Id = user.UserId }));

                    }
                }
            }
            catch (Exception)
            {
            }
            return View("UpdateUser");
        }
        #endregion

        #region Update User Profiles Avatar

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult UpdateProfileAvatar(HttpPostedFileBase UpdateUserAvatar, string UserId)
        {

            try
            {
                //Int64 userid = new UserSession().getUserSession().UserId;
                string USER_id = new SecurityManager().DecodeString(UserId);
                User Old_userObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (UpdateUserAvatar != null)
                {
                    savedFileName = uploadProfileImage(UpdateUserAvatar);
                    if (!string.IsNullOrWhiteSpace(savedFileName))
                    {
                        string filename = new User().ChangeProfilePhoto(Old_userObj.UserId, savedFileName);
                        //New_userObj.ProfilePhoto = savedFileName;
                        deleteUploadedImage(filename);
                        User retObj = new User().AuthnticateUser(Old_userObj);
                        if (retObj != null)
                        {
                            new UserSession().SetUserSession(retObj);
                        }


                    }
                }
                Old_userObj = null;
            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "UpdateProfiles", action = "UpdateUser", Id = UserId }));
        }
        public string uploadProfileImage(HttpPostedFileBase file)
        {
            try
            {
                string fileName = "";
                string retFileName = "";
                if (file != null)
                {
                    fileName = new UserSession().getUserSession().UserId + "_" + DateTime.UtcNow.ToString();
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    fileName = fileName + Path.GetFileName(file.FileName);
                    retFileName = fileName;

                    string userFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES);
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    string filePath = userFolder + "\\" + fileName;
                    file.SaveAs(filePath);


                    file.InputStream.Flush();
                    file.InputStream.Close();
                    file.InputStream.Dispose();

                    GenerateThumbNail(retFileName);
                }
                return retFileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public void deleteUploadedImage(string imagePath)
        {
            try
            {
                string filePathName = Constants.UPLOADED_IMAGES + "/" + imagePath;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }

                filePathName = Constants.UPLOADED_IMAGES_THUMBNAILS + "/" + imagePath;
                TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }

            }
            catch (Exception)
            {
            }
        }
        private void GenerateThumbNail(String imageName)
        {
            try
            {
                string userFolder = Constants.UPLOADED_IMAGES;
                string filePathName = userFolder + "/" + imageName;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(Server.MapPath("../" + filePathName));
                    Size thumbnailSize = GetThumbnailSize(uploadedImage);
                    System.Drawing.Image thumbNailImage = uploadedImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
                    uploadedImage.Dispose();

                    String thumbnailFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_THUMBNAILS);
                    if (!Directory.Exists(thumbnailFolder))
                    {
                        Directory.CreateDirectory(thumbnailFolder);
                    }

                    String filePath = thumbnailFolder + "\\" + imageName;


                    thumbNailImage.Save(filePath);
                    thumbNailImage.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }
        static Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = Constants.THUMBNAIL_WIDTH;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

        //static Size GetThumbnailSize_for_HomePage(Image original)
        //{
        //    // Maximum size of any dimension.
        //    const int maxPixels = Constants.THUMBNAIL_WIDTH_SHOPHOME;

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

        #region Update Shop
        public ActionResult UpdateShop(string id)
        {
            ViewBag.TitleDes = "Free shop advertisement";

            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_PROFILE;
            ViewData[Constants.MENU] = Constants.EDIT_SHOP_PROFILE;

            Int64 userid = new UserSession().getUserSession().UserId;
            User user = new User().GetUser(userid);
            ViewData.Model = user;
            return View();
        }
        [HttpPost][ValidateInput(false)]
        public ActionResult UpdateShopInfo(string txtShopName, string txtShopCountry, string txtShopCity, string txtShopAddress, string txtMapUrl, string txtShopDescription, string userId)
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

                    //Int64 userid = new UserSession().getUserSession().UserId;

                    User userObj = new User();
                    userObj.UserId =Convert.ToInt64(userId);
                    userObj.ShopName = txtShopName;
                    userObj.ShopCountry = txtShopCountry;
                    userObj.ShopCity = txtShopCity;
                    userObj.ShopAddress = txtShopAddress;
                    userObj.ShopMapUrl = txtMapUrl;
                    userObj.ShopDescription =txtShopDescription;
                    userObj.Shop_Last_Updated_On = DateTime.UtcNow;
                    userObj.Is_ShopCreated = true;
                    User retShop = new User().CreateShop(userObj);
                    if (retShop == null)
                    {
                        validInput = false;
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Error", "Error occured during process Please try again");
                    }
                    else
                    {
                        string USER_ID = new ShopOnline.Common.SecurityManager().EncodeString(Convert.ToString(retShop.UserId));
                        new UserSession().SetUserSession(retShop);
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "UpdateProfiles", action = "UpdateShop", Id = USER_ID }));
                    }
                }
            }
            catch (Exception)
            {
            }

            return View("UpdateShop");
        }
        #endregion

        #region Change Shop Profile Avatar

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult ChangeShopProfileAvatar(HttpPostedFileBase UpdateShopAvatar, string UserId)
        {

            try
            {
                //Int64 userid = new UserSession().getUserSession().UserId;
                string USER_id = new SecurityManager().DecodeString(UserId);
                User Old_userObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (UpdateShopAvatar != null)
                {
                    savedFileName = uploadShopProfileImage(UpdateShopAvatar);
                    if (!string.IsNullOrWhiteSpace(savedFileName))
                    {
                        string filename = new User().ChangeProfileShopPhoto(Old_userObj.UserId, savedFileName);
                        //New_userObj.ProfilePhoto = savedFileName;
                        deleteUploadedShopImage(filename);
                        User retObj = new User().AuthnticateUser(Old_userObj);
                        if (retObj != null)
                        {
                            new UserSession().SetUserSession(retObj);
                        }


                    }
                }
                Old_userObj = null;
            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "UpdateProfiles", action = "UpdateShop", Id = UserId }));
        }

        public string uploadShopProfileImage(HttpPostedFileBase file)
        {
            try
            {
                string fileName = "";
                string retFileName = "";
                if (file != null)
                {
                    fileName = new UserSession().getUserSession().UserId + "_" + DateTime.UtcNow.ToString();
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    fileName = fileName + Path.GetFileName(file.FileName);
                    retFileName = fileName;

                    string userFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_SHOP);
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    string filePath = userFolder + "\\" + fileName;
                    file.SaveAs(filePath);


                    file.InputStream.Flush();
                    file.InputStream.Close();
                    file.InputStream.Dispose();

                    GenerateShopThumbNail(retFileName);
                    //GenerateShopThumbNail_ShopHome(retFileName);
                }
                return retFileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private void GenerateShopThumbNail(String imageName)
        {
            try
            {
                string userFolder = Constants.UPLOADED_IMAGES_SHOP;
                string filePathName = userFolder + "/" + imageName;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(Server.MapPath("../" + filePathName));
                    Size thumbnailSize = GetThumbnailSize(uploadedImage);
                    System.Drawing.Image thumbNailImage = uploadedImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
                    uploadedImage.Dispose();

                    String thumbnailFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_THUMBNAILS_SHOP);
                    if (!Directory.Exists(thumbnailFolder))
                    {
                        Directory.CreateDirectory(thumbnailFolder);
                    }

                    String filePath = thumbnailFolder + "\\" + imageName;


                    thumbNailImage.Save(filePath);
                    thumbNailImage.Dispose();

                }
            }
            catch (Exception)
            {
            }
        }
        //private void GenerateShopThumbNail_ShopHome(String imageName)
        //{
        //    try
        //    {
        //        string userFolder = Constants.UPLOADED_IMAGES_SHOP;
        //        string filePathName = userFolder + "/" + imageName;
        //        FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
        //        if (TheFile.Exists)
        //        {
        //            System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(Server.MapPath("../" + filePathName));
        //            Size thumbnailSize = GetThumbnailSize_for_HomePage(uploadedImage);
        //            System.Drawing.Image thumbNailImage = uploadedImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
        //            uploadedImage.Dispose();

        //            String thumbnailFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_THUMBNAILS_SHOPHOME);
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
        public void deleteUploadedShopImage(string imagePath)
        {
            try
            {
                string filePathName = Constants.UPLOADED_IMAGES_SHOP + "/" + imagePath;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }

                filePathName = Constants.UPLOADED_IMAGES_THUMBNAILS_SHOP + "/" + imagePath;
                TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }

                //filePathName = Constants.UPLOADED_IMAGES_THUMBNAILS_SHOPHOME + "/" + imagePath;
                //TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                //if (TheFile.Exists)
                //{
                //    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                //}

            }
            catch (Exception)
            {
            }
        }

        #endregion

    }
}
