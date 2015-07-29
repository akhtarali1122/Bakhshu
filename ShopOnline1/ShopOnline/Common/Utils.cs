using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Models;
using System.Web.Configuration;
using System.Text.RegularExpressions;
namespace ShopOnline.Common
{
    public class Utils
    {
        public static OperationMessageEntity getErrorMessage(string messageTitle, string messageText)
        {
            try
            {

                OperationMessageEntity messageEntity = new OperationMessageEntity();

                messageEntity.MessageType = Convert.ToInt32(OperationMessageType.Error);
                messageEntity.MessageTitle = messageTitle;
                messageEntity.MessageText = messageText;

                return messageEntity;

            }
            catch (Exception)
            {
                return new OperationMessageEntity();
            }

        }
        public static OperationMessageEntity getNoticeMessage(string messageTitle, string messageText)
        {
            try
            {

                OperationMessageEntity messageEntity = new OperationMessageEntity();

                messageEntity.MessageType = Convert.ToInt32(OperationMessageType.Notics);
                messageEntity.MessageTitle = messageTitle;
                messageEntity.MessageText = messageText;

                return messageEntity;

            }
            catch (Exception)
            {
                return new OperationMessageEntity();
            }

        }
        public static OperationMessageEntity getInformationMessage(string messageTitle, string messageText)
        {
            try
            {

                OperationMessageEntity messageEntity = new OperationMessageEntity();

                messageEntity.MessageType = Convert.ToInt32(OperationMessageType.Information);
                messageEntity.MessageTitle = messageTitle;
                messageEntity.MessageText = messageText;

                return messageEntity;

            }
            catch (Exception)
            {
                return new OperationMessageEntity();
            }

        }
        public static OperationMessageEntity getSuccessMessage(string messageTitle, string messageText)
        {
            try
            {

                OperationMessageEntity messageEntity = new OperationMessageEntity();

                messageEntity.MessageType = Convert.ToInt32(OperationMessageType.Success);
                messageEntity.MessageTitle = messageTitle;
                messageEntity.MessageText = messageText;

                return messageEntity;

            }
            catch (Exception)
            {
                return new OperationMessageEntity();
            }

        }
        public static string getConfig(string keyName)
        {
            return WebConfigurationManager.AppSettings.Get(keyName);
        }
        public static string getSiteUrl()
        {
            try
            {
                if (RootPath != null && !RootPath.Equals(string.Empty))
                {
                    return RootPath;
                }
                else
                {
                    return WebConfigurationManager.AppSettings.Get(Constants.SITE_URL);
                }
            }
            catch (Exception)
            {
                return WebConfigurationManager.AppSettings.Get(Constants.SITE_URL);
            }
        }
        public static string getSiteName()
        {
            try
            {
                return WebConfigurationManager.AppSettings.Get(Constants.SITE_NAME);
            }
            catch (Exception)
            {
                return WebConfigurationManager.AppSettings.Get(Constants.SITE_NAME);
            }
        }
        public static string RootPath
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Host;// +
                //(HttpContext.Current.Request.Url.Port != 80 && HttpContext.Current.Request.Url.Port > 0 ? ":" + HttpContext.Current.Request.Url.Port : string.Empty);
            }
        }
        public static string GetEmailTemplate(string strSalutation, string messageBody)
        {
            try
            {
                string retHtml = "<div style='border:Solid 5px #efefef;font:14px/1.5em Arial,Helvetica,sans-serif'>";
                retHtml = retHtml + "<div style='height:30px;background-color:#efefef;padding-left:5px;font-size:20px;padding-top:5px'>";
                retHtml = retHtml + "<b>doctorarena.com</b>";
                retHtml = retHtml + "</div>";
                retHtml = retHtml + "<div style='padding:20px'>";
                if (!string.IsNullOrEmpty(strSalutation))
                {
                    retHtml = retHtml + "<div style='font-size:16px;'>";
                    retHtml = retHtml + "<b>Dear " + strSalutation + "</b>,";
                    retHtml = retHtml + "</div>";
                }
                else
                {
                    retHtml = retHtml + "<div style='font-size:16px;'>";
                    retHtml = retHtml + "<b>Hello,";
                    retHtml = retHtml + "</div>";
                }
                retHtml = retHtml + "<div style='padding-top:20px;'>";
                retHtml = retHtml + messageBody;
                retHtml = retHtml + "</div>";

                retHtml = retHtml + "<div style='padding-top:20px'>";
                retHtml = retHtml + "Kind Regards,";
                retHtml = retHtml + "</div>";
                retHtml = retHtml + "<div>";
                retHtml = retHtml + "Support Team";
                retHtml = retHtml + "</div>";

                retHtml = retHtml + "</div>";
                retHtml = retHtml + "<div style='height:15px;background-color:#efefef;padding:7px;text-align:center;font-size:12px'>";
                retHtml = retHtml + "This is an automated message from <a href='http://www.doctorarena.com'>www.doctorarena.com</a>";
                retHtml = retHtml + "</div>";
                retHtml = retHtml + "</div>";

                return retHtml;

            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public static string getCaptchaCode()
        {
            string captchaCode = Guid.NewGuid().ToString();
            if (captchaCode.Length > 6)
            {
                captchaCode = Guid.NewGuid().ToString().Substring(1, 6);
            }
            captchaCode = new SecurityManager().EncodeString(captchaCode);
            return captchaCode;
        }
        public static string SiteDomainName
        {
            get
            {
                return (HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.Port != 80 && HttpContext.Current.Request.Url.Port > 0 ? ":" + HttpContext.Current.Request.Url.Port : string.Empty)).Replace("http://", "").Replace("www.", "").Replace(".ebizline.pk", "");
            }
        }
        public static string getShortString(string str, int strLength)
        {
            try
            {
                if (str == null)
                {
                    return string.Empty;
                }
                if (str.Length > strLength)
                {
                    str = str.Substring(0, strLength - 1) + " ...";
                }

                return str;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static DateTime GetDefaultDate()
        {
            return new DateTime(1900, 1, 1);
        }
        public static DateTime GetCurrentDateTimeByTimeZone(string timeZoneId)
        {
            if (!string.IsNullOrWhiteSpace(timeZoneId))
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime CurrDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                return CurrDateTime;
            }

            return new DateTime(1900, 1, 1);
        }
        public static string setEncyption(string reqParam)
        {
            try
            {
                if (reqParam != null)
                {
                    reqParam = reqParam.Replace("/", "mdSlashzib0001");
                    reqParam = reqParam.Replace("+", "mdPluszib0001");
                    reqParam = reqParam.Replace("=", "mdEqualzib0001");
                    reqParam = reqParam.Replace(" ", "mdSpacezib0002");
                    reqParam = reqParam.Replace("~", "mdTiledzib0003");
                    reqParam = reqParam.Replace("`", "mdSmallTiledzib0004");
                    reqParam = reqParam.Replace("!", "mdNotzib0005");
                    reqParam = reqParam.Replace("@", "mdAtRateOfzib0006");
                    reqParam = reqParam.Replace("#", "mdHashzib0007");
                    reqParam = reqParam.Replace("%", "mdPercentzib0008");
                    reqParam = reqParam.Replace("^", "mdPowerzib0009");
                    reqParam = reqParam.Replace("&", "mdAndzib00010");
                }
                return reqParam;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string getDecription(string reqParam)
        {
            try
            {
                if (reqParam != null)
                {
                    reqParam = reqParam.Replace("mdSlashzib0001", "/");
                    reqParam = reqParam.Replace("mdPluszib0001", "+");
                    reqParam = reqParam.Replace("mdEqualzib0001", "=");
                    reqParam = reqParam.Replace("mdSpacezib0002", " ");
                    reqParam = reqParam.Replace("mdTiledzib0003", "~");
                    reqParam = reqParam.Replace("mdSmallTiledzib0004", "`");
                    reqParam = reqParam.Replace("mdNotzib0005", "!");
                    reqParam = reqParam.Replace("mdAtRateOfzib0006", "@");
                    reqParam = reqParam.Replace("mdHashzib0007", "#");
                    reqParam = reqParam.Replace("mdPercentzib0008", "%");
                    reqParam = reqParam.Replace("mdPowerzib0009", "^");
                    reqParam = reqParam.Replace("mdAndzib00010", "&");
                }
                return reqParam;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool IsImageFile(string contentType)
        {
            switch (contentType)
            {
                case "image/x-png":
                case "image/pjpeg":
                case "image/gif":
                case "image/tiff":
                    return true;
                default:
                    return false;
            }
        }
        public static string getMonthStr(int month)
        {
            try
            {

                if ((month == 1) || (month == 01))
                {
                    return "January";
                }
                else if ((month == 2) || (month == 02))
                {
                    return "Fabruary";
                }
                else if ((month == 3) || (month == 03))
                {
                    return "March";
                }
                else if ((month == 4) || (month == 04))
                {
                    return "April";
                }
                else if ((month == 5) || (month == 05))
                {
                    return "May";
                }
                else if ((month == 6) || (month == 06))
                {
                    return "June";
                }
                else if ((month == 7) || (month == 07))
                {
                    return "July";
                }
                else if ((month == 8) || (month == 08))
                {
                    return "August";
                }
                else if ((month == 9) || (month == 09))
                {
                    return "September";
                }
                else if (month == 10)
                {
                    return "October";
                }
                else if (month == 11)
                {
                    return "November";
                }
                else if (month == 12)
                {
                    return "December";
                }

                return string.Empty;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static Boolean isValidEmail(string Email)
        {
            try
            {

                string pattern = @"^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$";

                Match match = Regex.Match(Email, pattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string getRandomCode(int StrLength)
        {
            string AccessCode = DateTime.UtcNow.Month.ToString() + DateTime.UtcNow.Day.ToString();// +DateTime.UtcNow.Year.ToString();

            string randomStr = Guid.NewGuid().ToString();
            if (randomStr.Length > StrLength)
            {
                randomStr = randomStr.Substring(1, StrLength - 1);
            }
            AccessCode = AccessCode + randomStr;


            return AccessCode.ToUpper();
        }
        public static string getUserRoleStr(int roleId)
        {
            try
            {
                if (roleId == Convert.ToInt32(UserRolesType.Admin))
                {
                    return "Admin";
                }
                else if (roleId == Convert.ToInt32(UserRolesType.RegisteredUser))
                {
                    return "Registered User";
                }
                return string.Empty;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string getStatusStr(int statusId)
        {
            try
            {
                if (statusId == Convert.ToInt32(UserStatusType.Active))
                {
                    return "Active(Running)";
                }
                else if (statusId == Convert.ToInt32(UserStatusType.InActive))
                {
                    return "In-Active";
                }
                else if (statusId == Convert.ToInt32(UserStatusType.Blocked))
                {
                    return "Blocked";
                }
                else if (statusId == Convert.ToInt32(UserStatusType.Suspended))
                {
                    return "Suspended";
                }

                return string.Empty;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static List<UserRolesEntity> getUserRolesList()
        {
            List<UserRolesEntity> rolesList = new List<UserRolesEntity>();

            UserRolesEntity role = new UserRolesEntity();
            role.Role_Id = Convert.ToInt32(UserRolesType.RegisteredUser);
            role.Role_Title = "Paid User";
            rolesList.Add(role);


            //role = new UserRolesEntity();
            //role.Role_Id = Convert.ToInt32(UserRolesType.FreeUser);
            //role.Role_Title = "Free User";
            //rolesList.Add(role);

            return rolesList;
        }
        public static List<UserRolesEntity> getUserRolesListOnRegisteration()
        {
            List<UserRolesEntity> rolesList = new List<UserRolesEntity>();

            UserRolesEntity role = new UserRolesEntity();
            role.Role_Id = Convert.ToInt32(UserRolesType.RegisteredUser);
            role.Role_Title = "Paid User";
            rolesList.Add(role);


            //role = new UserRolesEntity();
            //role.Role_Id = Convert.ToInt32(UserRolesType.FreeUser);
            //role.Role_Title = "Free User";
            //rolesList.Add(role);

            return rolesList;
        }
        public static string formatAddressStr(string address, string city, string state, string zipcode)
        {
            try
            {

                string strAddress = "";
                Boolean findAddress = false;
                if (!string.IsNullOrEmpty(address))
                {
                    strAddress = address;
                    findAddress = true;
                }
                else
                {
                    findAddress = false;
                }

                if (!string.IsNullOrEmpty(city))
                {
                    if (findAddress == true)
                    {
                        strAddress = strAddress + ", " + city;
                    }
                    else
                    {
                        strAddress = strAddress + city;
                    }
                    findAddress = true;
                }
                else
                {
                    findAddress = false;
                }
                if (!string.IsNullOrEmpty(state))
                {
                    if (findAddress == true)
                    {
                        strAddress = strAddress + ", " + state;
                    }
                    else
                    {
                        strAddress = strAddress + state;
                    }
                    findAddress = true;
                }
                else
                {
                    findAddress = false;
                }
                if (!string.IsNullOrEmpty(zipcode))
                {
                    if (findAddress == true)
                    {
                        strAddress = strAddress + ", " + zipcode;
                    }
                    else
                    {
                        strAddress = strAddress + zipcode;
                    }
                    findAddress = true;
                }


                return strAddress;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string formatContactNosStr(string PhoneNo, string CellNo)
        {
            try
            {

                string strNos = "";

                if (!string.IsNullOrWhiteSpace(PhoneNo))
                {
                    strNos = PhoneNo;
                }

                if (!string.IsNullOrWhiteSpace(CellNo))
                {
                    if (strNos == "")
                    {
                        strNos = CellNo;
                    }
                    else
                    {
                        strNos = strNos + ", " + CellNo;
                    }
                }
                return strNos;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string formatContactNosStr(string PhoneNo, string CellNo, string Fax, string email)
        {
            try
            {

                string strNos = "";

                if (!string.IsNullOrWhiteSpace(PhoneNo))
                {
                    strNos = PhoneNo;
                }

                if (!string.IsNullOrWhiteSpace(CellNo))
                {
                    if (strNos == "")
                    {
                        strNos = CellNo;
                    }
                    else
                    {
                        strNos = strNos + ", " + CellNo;
                    }
                }

                if (!string.IsNullOrWhiteSpace(Fax))
                {
                    if (strNos == "")
                    {
                        strNos = Fax;
                    }
                    else
                    {
                        strNos = strNos + ", " + Fax;
                    }
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    if (strNos == "")
                    {
                        strNos = email;
                    }
                    else
                    {
                        strNos = strNos + ", " + email;
                    }
                }

                return strNos;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}