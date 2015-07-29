using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class ViewShopProfileController : Controller
    {
        //
        // GET: /ViewShopProfile/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SeeShopProfile(string id)
        {
            ViewBag.TitleDes = "Free shop advertisement";
            Int64 User_id = Convert.ToInt64(new SecurityManager().DecodeString(id));
            ShopProfileEntity SPE = new ShopProfileEntity();

            SPE.ShopImagesList = new Shop_Image().GetShopImagesList(User_id);
            SPE.ShopNewsList = new Shop_New().GetShopNewsList(User_id);
            SPE.ShopUserList = new User().GetUser(User_id);
            SPE.ShopItemsList = new Shop_Item().GetShopItemList(User_id);

            ViewData.Model = SPE;
            return View();
        }


    }
}
