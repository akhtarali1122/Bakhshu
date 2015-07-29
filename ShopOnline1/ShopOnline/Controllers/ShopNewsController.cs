using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Filters;
using ShopOnline.Common;
using ShopOnline.Models;
using System.Web.Routing;

namespace ShopOnline.Controllers
{
    
    public class ShopNewsController : Controller
    {
        //
        // GET: /ShopNews/

        public ActionResult Index()
        {
            return View();
        }
        [SessionExpireFilter]
        public ActionResult AddNewsForm(string RecSaveOperation, string hidNewsId)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(hidNewsId))
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
                    ViewData[Constants.MENU] = Constants.MANAGE_NEWS;

                    string News_Id = new SecurityManager().DecodeString(hidNewsId);

                    Shop_New SN = new Shop_New().GetShopNews(Convert.ToInt64(News_Id));
                    ViewData["txtNewsHeadLine"] = SN.News_HeadLine;
                    ViewData["txtNewsDetail"] = SN.News_Detail;
                    ViewData["hidNewsId"] = hidNewsId;
                }
                else
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
                    ViewData[Constants.MENU] = Constants.ADD_NEWS;

                    ViewBag.TitleDes = "Free shop advertisement";
                    if (!String.IsNullOrEmpty(RecSaveOperation))
                    {
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record saved successfully.");
                    }
                }
            }
            catch(Exception ex)
            { }
            return View("AddNews");
        }
        [HttpPost]
        [ValidateInput(false)]
        [SessionExpireFilter]
        public ActionResult AddNews(string txtNewsHeadLine, string txtNewsDetail, string hidNewsId)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(hidNewsId))
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
                    ViewData[Constants.MENU] = Constants.MANAGE_NEWS;
                }
                else
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
                    ViewData[Constants.MENU] = Constants.ADD_NEWS;
                }

                ViewBag.TitleDes = "Free shop advertisement";

                ViewData["txtNewsHeadLine"] = txtNewsHeadLine;
                ViewData["txtNewsDetail"] = txtNewsDetail;
                ViewData["hidNewsId"] = hidNewsId;

                Boolean isValid = true;

                if (string.IsNullOrWhiteSpace(txtNewsHeadLine))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Enter News HeadLine");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtNewsDetail))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Enter News Detail");
                    isValid = false;
                }

                if (isValid == true)
                {
                    Shop_New SN = new Shop_New();
                    SN.User_Id = new ShopOnline.Filters.UserSession().getUserSession().UserId;
                    SN.News_HeadLine = txtNewsHeadLine;
                    SN.News_Detail = txtNewsDetail;


                    if (!string.IsNullOrWhiteSpace(hidNewsId))
                    {
                        SN.NewsId = Convert.ToInt64(new SecurityManager().DecodeString(hidNewsId));
                    }
                    if (SN.NewsId > 0)
                    {
                        new Shop_New().UpdateShopNews(SN);
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopNews", action = "ManageShopNews", RecUpdateOperation = "1" }));
                    }
                    else
                    {
                        SN.Created_On = DateTime.UtcNow;
                        Shop_New shop_news = new Shop_New().AddNews(SN);
                        if (shop_news != null)
                        {
                            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopNews", action = "AddNewsForm", RecSaveOperation = "1" }));
                        }
                        else
                        {
                            ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Sorry record could not be saved");
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View("AddNews");
        }
        [SessionExpireFilter]
        public ActionResult ManageShopNews(string RecDelOperation, string RecDelErrorOperation, string RecUpdateOperation)
        {
            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
            ViewData[Constants.MENU] = Constants.MANAGE_NEWS;
            if (!String.IsNullOrEmpty(RecDelOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record Deleted successfully.");
            }
            if (!String.IsNullOrEmpty(RecDelErrorOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Success", "Sorry Record Could not be Deleted.");
            }
            if (!String.IsNullOrEmpty(RecUpdateOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record Updated successfully.");
            }
            ViewBag.TitleDes = "Free shop advertisement";
            ViewData.Model = new Shop_New().GetShopNewsList(new ShopOnline.Filters.UserSession().getUserSession().UserId);

            return View();
        }
        //public ActionResult EditShopNews(string NewsId)
        //{
        //    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_NEWS;
        //    ViewData[Constants.MENU] = Constants.MANAGE_NEWS;

        //    ViewBag.TitleDes = "Free shop advertisement";

        //    string News_Id = new SecurityManager().DecodeString(NewsId);

        //    ViewData.Model = new Shop_New().GetShopNews(Convert.ToInt64(News_Id));

        //    return View("EditNews");
        //}
        public ActionResult DeleteNews(string NewsId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewsId))
                {
                    string News_Id = new SecurityManager().DecodeString(NewsId);
                    Boolean retItem = new Shop_New().DeleteNews(Convert.ToInt64(News_Id));
                    if (retItem == true)
                    {
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopNews", action = "ManageShopNews", RecDelOperation = "1" }));
                    }
                    else
                    {
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopNews", action = "ManageShopNews", RecDelErrorOperation = "1" }));
                    }
                }
            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopNews", action = "ManageShopNews" }));
        }

    }
}
