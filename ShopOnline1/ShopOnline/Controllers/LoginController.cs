using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShopOnline.Filters;
using ShopOnline.Models;
using ShopOnline.Common;

namespace ShopOnline.Controllers
{
    public class LoginController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.TitleDes = "Free shop advertisement";
            //if (new UserSession().isUserLogin() == false)
            //{
                return View("Login");
            //}
            //else
            //{
            //    return RedirectToRoute(new RouteValueDictionary(new { controller = "Home", action = "ShopIndex" }));
            //}
        }

        [HttpPost]
        public ActionResult doLogin(string txtusername, string txtpassword)
        {
            try
            {

                ViewData["txtLoginName"] = txtusername;
                ViewData["txtPassword"] = txtpassword;
                if (!string.IsNullOrEmpty(txtusername) && !string.IsNullOrEmpty(txtpassword))
                {

                    User userObj = new User();
                    userObj.UserLoginName = txtusername;
                    userObj.UserPassword = txtpassword.Trim();
                    userObj.UserStatus = Convert.ToInt32(UserStatusType.Active);
                    User retObj = new User().AuthnticateUser(userObj);
                    if (retObj != null)
                    {
                        new UserSession().SetUserSession(retObj);
                        if (retObj.Is_ShopCreated == true)
                        {
                            return RedirectToRoute(new RouteValueDictionary(new { controller = "Home", action = "DashBoard" }));
                        }
                        else
                        {
                            return RedirectToRoute(new RouteValueDictionary(new { controller = "Register", action = "ShopRegister" }));
                        }
                    }
                    else
                    {
                        ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Login Failed", "Please enter correct username and password");
                    }
                }
                else
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter username and password");
                }
            }
            catch (Exception)
            {
            }
            return View("Login");
        }


        public ActionResult Logout()
        {
            new UserSession().expireSession();
            return RedirectToAction("Index", "Home");
        }


    }
}
