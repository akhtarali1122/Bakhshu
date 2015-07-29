using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using ShopOnline.Models;
using ShopOnline.Filters;
using System.Web.Routing;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return RedirectToRoute(new RouteValueDictionary(new { controller = "Home", action = "ShopIndex" }));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ShopCreatedFilter]
        [SessionExpireFilter]
        public ActionResult DashBoard()
        {
            ViewBag.TitleDes = "Free shop advertisement";
            ViewData[Constants.MENU] = Constants.MENU_DASHBOARD;
          return View();
        }
        public ActionResult ShopIndex()
        {
            ViewBag.TitleDes = "Free shop advertisement";
            

            ShopUserEntities SUE = new ShopUserEntities();
            
            SUE.ShopNewsList = new Shop_New().getAllNews();
            SUE.ShopUserList = new User().getAllUsers();
            SUE.ShopImagesList = new Shop_Image().getAllImages();

            ViewData.Model = SUE;

            //List<Shop_New> sn = new Shop_New().getAllNews();
            //List<Shop_Image> si = new Shop_Image().getAllImages();
            //List<User> u = new User().getAllUsers();


            return View();
        }
    }
}
