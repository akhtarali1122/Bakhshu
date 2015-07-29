using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Filters;
using ShopOnline.Models;
using ShopOnline.Common;
using System.Web.Routing;
using System.Drawing;
using System.IO;

namespace DairyFarmManagement.Controllers
{
    public class SettingsController : Controller
    {
        protected String SmtpUserName
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_USER_NAME_KEY) ? String.Empty : Constants.SMTP_USER_NAME_KEY; }
        }

        [SessionExpireFilter]
        public ActionResult ChangePasswordForm()
        {

            ViewBag.TitleDes = "Free shop advertisement";

            return View("ChangePassword");
        }

        [SessionExpireFilter]
        public ActionResult ChangePassword(string pwdCurrentPassword, string pwdNewPassword, string pwdConfirmPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pwdCurrentPassword) || string.IsNullOrEmpty(pwdNewPassword) || string.IsNullOrEmpty(pwdConfirmPassword))
                {
                    OperationMessageEntity objMsg = new OperationMessageEntity();
                    objMsg.MessageType = Convert.ToInt32(OperationMessageType.Error);
                    objMsg.MessageTitle = "Error";
                    objMsg.MessageText = "Please enter Password";

                    ViewData[Constants.OPERATIONALMESSAGE] = objMsg;
                }
                else if (!pwdNewPassword.Equals(pwdConfirmPassword))
                {
                    OperationMessageEntity objMsg = new OperationMessageEntity();
                    objMsg.MessageType = Convert.ToInt32(OperationMessageType.Error);
                    objMsg.MessageTitle = "Error";
                    objMsg.MessageText = "Password and Confirm Password does not match";

                    ViewData[Constants.OPERATIONALMESSAGE] = objMsg;
                }
                else if (!pwdCurrentPassword.Equals(new UserSession().getUserSession().UserPassword))
                {
                    OperationMessageEntity objMsg = new OperationMessageEntity();
                    objMsg.MessageType = Convert.ToInt32(OperationMessageType.Error);
                    objMsg.MessageTitle = "Error";
                    objMsg.MessageText = "Current password does not matched";

                    ViewData[Constants.OPERATIONALMESSAGE] = objMsg;
                }
                else
                {
                    new User().ChangeUserPassword(new UserSession().getUserSession().UserId, pwdNewPassword);
                    User user = new User().GetUser(new UserSession().getUserSession().UserId);
                    new UserSession().SetUserSession(user);
                    OperationMessageEntity objMsg = new OperationMessageEntity();
                    objMsg.MessageType = Convert.ToInt32(OperationMessageType.Success);
                    objMsg.MessageTitle = "Success";
                    objMsg.MessageText = "Password changed successfully";

                    ViewData[Constants.OPERATIONALMESSAGE] = objMsg;
                }
                return View("ChangePassword");
            }
            catch (Exception)
            {
                return View("ChangePassword");
            }
        }



    }
}
