using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Models;
//using System.Web.SessionState;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Xml.Linq;
//using System.Data;
//using System.Configuration;

namespace ShopOnline.Filters
{
    public class UserSession: System.Web.UI.Page
    {
        public void SetUserSession(User user)
        {
            Session["user"] = user;
        }
        public User getUserSession()
        {
            if (Session["user"] != null)
            {
                return (User)Session["user"];
            }
            else
            {
                return null;
            }
        }
        public Boolean isUserLogin()
        {
            if (Session["user"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean EverLogin_User()
        {
            if (Session["user"] != null)
            {
                if (new UserSession().getUserSession().Is_ShopCreated == true)
                {
                    return true;
                }
                else
                    return false;
            }
             return false;
            
        }
        public void expireSession()
        {
            Session["user"] = null;
            Session.Abandon();
        }
    }
}