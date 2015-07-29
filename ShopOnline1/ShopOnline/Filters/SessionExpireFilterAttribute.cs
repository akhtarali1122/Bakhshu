using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Web.Mvc;

namespace ShopOnline.Filters
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (new UserSession().isUserLogin() == false)
            {

                filterContext.Result = new RedirectResult("~/Login/Index");
                return;

            }

            base.OnActionExecuting(filterContext);
        }
    }
}