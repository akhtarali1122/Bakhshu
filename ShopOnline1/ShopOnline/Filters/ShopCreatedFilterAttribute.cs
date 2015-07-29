using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Filters
{
    public class ShopCreatedFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (new UserSession().EverLogin_User() == false)
            {

                filterContext.Result = new RedirectResult("~/Register/ShopRegister");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}