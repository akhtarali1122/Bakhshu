using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using ShopOnline.Common;

namespace ShopOnline.Filters
{
    public class AdminSessionFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (new UserSession().isUserLogin() == false)
            {

                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            if (new UserSession().isUserLogin() == true && new UserSession().getUserSession().UserRoleId != Convert.ToInt32(UserRolesType.Admin))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }


            base.OnActionExecuting(filterContext);
        }
    }  
}
