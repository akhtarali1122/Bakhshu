using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using ShopOnline.Filters;
using ShopOnline.Models;
using System.IO;
using System.Web.Routing;
using System.Drawing;

namespace ShopOnline.Controllers
{
    [SessionExpireFilter]
    public class ShopImagesController : Controller
    {
        //ShopImages/Index
        // GET: /ShopImages/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        [ShopCreatedFilter]
        public ActionResult Index(string RecDelOperation, string RecSaveOperation, string Id)
        {
            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_PICTURES;
            ViewData[Constants.MENU] = Constants.EDIT_PICTURES;

            ViewBag.TitleDes = "Free shop advertisement";
            try
            {

                if (!String.IsNullOrEmpty(RecDelOperation))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record deleted successfully.");
                }
                if (!String.IsNullOrEmpty(RecSaveOperation))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record saved successfully.");
                }

                if (!string.IsNullOrWhiteSpace(Id))
                {
                    string User_id = new SecurityManager().DecodeString(Id);
                    User user = new User().GetUser(Convert.ToInt64(User_id));
                    if (user != null)
                    {
                        user.Shop_Image_List = new Shop_Image().GetShopImagesList(user.UserId);
                        user.UserEntity = new User().GetUser(user.UserId);
                    }
                    ViewData.Model = user;
                    user = null;
                }
                else
                {
                    Int64 user_id = new UserSession().getUserSession().UserId;
                    User user = new User().GetUser(user_id);
                    if (user != null)
                    {
                        user.Shop_Image_List = new Shop_Image().GetShopImagesList(user.UserId);
                        user.UserEntity = new User().GetUser(user.UserId);
                    }
                    ViewData.Model = user;
                    user = null;
                }
                return View();
            }
            catch (Exception)
            {
            }
            return null;
        }

        #region Delete slider images

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult DeleteShopImage(string Shop_Image_Id)
        {
            try
            {
                string imageId = new SecurityManager().DecodeString(Shop_Image_Id);

                Shop_Image shopImage = new Shop_Image().GetShopImage(Convert.ToInt64(imageId));
                if (shopImage != null)
                {
                    Int64 UserId = shopImage.User_Id;
                    deleteUploadedImage(shopImage.Image_Path, UserId);
                    new Shop_Image().DeleteShopImage(shopImage.Shop_Image_Id);

                    string Enc_UserId = new SecurityManager().EncodeString(Convert.ToString(UserId));
                    return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = Enc_UserId }));

                }

            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "Home", action = "Dashboard" }));
        }
        public void deleteUploadedImage(string imagePath, Int64 ImageId)
        {
            try
            {
                string filePathName = Constants.UPLOADED_IMAGES_SHOP + "/" + ImageId + "/" + imagePath;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }

                filePathName = Constants.UPLOADED_IMAGES_THUMBNAILS_SHOP + "/" + ImageId + "/" + imagePath;
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

        #endregion

        #region upload shop images for slider


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult AddShopImage(HttpPostedFileBase ShopImageFile, string txtImageTitle, string chkSlider, string UserId)
        {

            try
            {
                string USER_id = new SecurityManager().DecodeString(UserId);
                User UserObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (ShopImageFile != null)
                {
                    savedFileName = uploadProfileImage(ShopImageFile, UserObj.UserId);
                    if (!string.IsNullOrWhiteSpace(savedFileName))
                    {

                        Shop_Image shopImage = new Shop_Image();
                        shopImage.User_Id = UserObj.UserId;
                        shopImage.Image_Title = txtImageTitle;
                        shopImage.Image_Path = savedFileName;
                        shopImage.Is_For_Home_Page_Slider = false;
                        shopImage.Created_On = DateTime.Now;
                        if (!string.IsNullOrWhiteSpace(chkSlider) && chkSlider.Equals("1"))
                        {
                            shopImage.Is_For_Home_Page_Slider = true;
                        }
                        shopImage.AddShopImage(shopImage);
                        shopImage = null;
                    }
                }
                UserObj = null;
            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = UserId }));

        }

        public string uploadProfileImage(HttpPostedFileBase file, Int64 UserId)
        {
            try
            {
                string fileName = "";
                string retFileName = "";
                if (file != null)
                {
                    fileName = new UserSession().getUserSession().UserId + "_" + UserId + "_" + DateTime.UtcNow.ToString();
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    fileName = fileName + Path.GetFileName(file.FileName);
                    retFileName = fileName;

                    string userFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_SHOP + "/" + UserId);
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    string filePath = userFolder + "\\" + fileName;
                    file.SaveAs(filePath);
                    GenerateThumbNailForSlider(retFileName, UserId);
                }
                return retFileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private void GenerateThumbNailForSlider(String imageName, Int64 UserId)
        {
            try
            {
                string userFolder = Constants.UPLOADED_IMAGES_SHOP + "/" + UserId;
                string filePathName = userFolder + "/" + imageName;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists)
                {
                    System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(Server.MapPath("../" + filePathName));
                    Size thumbnailSize = GetThumbnailSize(uploadedImage);
                    System.Drawing.Image thumbNailImage = uploadedImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
                    uploadedImage.Dispose();

                    String thumbnailFolder = Server.MapPath("../" + Constants.UPLOADED_IMAGES_THUMBNAILS_SHOP + "/" + UserId);
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
        #endregion

        #region

        #endregion

        #region Change Shop Profile Avatar

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult ChangeShopProfileAvatar(HttpPostedFileBase fileShopProfile, string UserId)
        {

            try
            {
                //Int64 userid = new UserSession().getUserSession().UserId;
                string USER_id = new SecurityManager().DecodeString(UserId);
                User Old_userObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (fileShopProfile != null)
                {
                    savedFileName = uploadShopProfileImage(fileShopProfile);
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
            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = UserId }));
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

        #region change User Profiles Avatar





        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult ChangeProfileAvatar(HttpPostedFileBase fileUserProfile, string UserId)
        {

            try
            {
                //Int64 userid = new UserSession().getUserSession().UserId;
                string USER_id = new SecurityManager().DecodeString(UserId);
                User Old_userObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (fileUserProfile != null)
                {
                    savedFileName = uploadProfileImage(fileUserProfile);
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
            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopImages", action = "Index", Id = UserId }));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult UpdateProfileAvatar(HttpPostedFileBase UpdateUserProfile, string UserId)
        {

            try
            {
                //Int64 userid = new UserSession().getUserSession().UserId;
                string USER_id = new SecurityManager().DecodeString(UserId);
                User Old_userObj = new User().GetUser(Convert.ToInt64(USER_id));
                string savedFileName = "";
                if (UpdateUserProfile != null)
                {
                    savedFileName = uploadProfileImage(UpdateUserProfile);
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

    }
}
