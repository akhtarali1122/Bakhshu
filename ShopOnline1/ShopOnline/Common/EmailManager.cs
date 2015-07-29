using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace ShopOnline.Common
{
    public class EmailManager
    {
        #region "Class Properties"
        protected String SmtpServer
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_SERVER_KEY) ? String.Empty : Constants.SMTP_SERVER_KEY; }
        }
        protected int SmtpPort
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_PORT_KEY) ? 25 : int.Parse(Constants.SMTP_PORT_KEY); }
        }
        protected Boolean IsSSLRequired
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_SSL_KEY) ? false : Constants.SMTP_SSL_KEY.Equals(Constants.TRUE); }
        }
        protected Boolean IsAuthenticationRequired
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_AUTH_KEY) ? false : Constants.SMTP_AUTH_KEY.Equals(Constants.TRUE); }
        }
        protected String SmtpUserName
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_USER_NAME_KEY) ? String.Empty : Constants.SMTP_USER_NAME_KEY; }
        }
        protected String SmtpUserPassword
        {
            get { return String.IsNullOrEmpty(Constants.SMTP_USER_PASS_KEY) ? String.Empty : Constants.SMTP_USER_PASS_KEY; }
        }

        #endregion

        #region "Public Methods"


        public Boolean SendEmail(String toAddress, String from, String subject, String msgBody)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, String.Empty, String.Empty, from, String.Empty, subject, msgBody, true, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String from, String subject, String msgBody, MailPriority msgPriority)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, String.Empty, String.Empty, from, String.Empty, subject, msgBody, true, msgPriority);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String addresseName, String from, String senderName, String subject, String msgBody, MailPriority msgPriority)
        {
            try
            {
                return SendEmail(toAddress, addresseName, String.Empty, String.Empty, from, senderName, subject, msgBody, true, msgPriority);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String from, String subject, String msgBody, Boolean isHtmlEmail)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, String.Empty, String.Empty, from, String.Empty, subject, msgBody, isHtmlEmail, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String ccAddress, String from, String subject, String msgBody)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, ccAddress, String.Empty, from, String.Empty, subject, msgBody, true, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String ccAddress, String from, String subject, String msgBody, Boolean isHtml)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, ccAddress, String.Empty, from, String.Empty, subject, msgBody, isHtml, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String ccAddress, String from, String subject, String msgBody, Boolean isHtml, MailPriority msgPriority)
        {
            try
            {
                return SendEmail(toAddress, String.Empty, ccAddress, String.Empty, from, String.Empty, subject, msgBody, isHtml, msgPriority);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String addresseName, String from, String senderName, String subject, String msgBody)
        {
            try
            {
                return SendEmail(toAddress, addresseName, String.Empty, String.Empty, from, senderName, subject, msgBody, true, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }
        public Boolean SendEmail(String toAddress, String addresseName, String from, String senderName, String subject, String msgBody, Boolean isHtmlEmail)
        {
            try
            {
                return SendEmail(toAddress, addresseName, String.Empty, String.Empty, from, senderName, subject, msgBody, isHtmlEmail, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }


        public Boolean SendEmailTo(String toAddress, String addresseName, String from, String senderName, String subject, String msgBody, Boolean isHtmlEmail)
        {
            try
            {
                return SendEmail(toAddress, addresseName, String.Empty, String.Empty, from, senderName, subject, msgBody, isHtmlEmail, MailPriority.Normal);
            }
            catch
            {
                return false;
            }
        }

  
        public Boolean SendEmail(String toAddress, String addresseName, String ccAddress, String ccName, String from, String senderName, String subject, String msgBody, Boolean isHtmlEmail, MailPriority msgPriority)
        {
            try
            {
                MailMessage email = new MailMessage();
                SmtpClient client = new SmtpClient();

                if (senderName != String.Empty)
                    email.From = new MailAddress(from, senderName);
                else
                    email.From = new MailAddress(from);

                if (addresseName != String.Empty)
                     email.To.Add(new MailAddress(toAddress, addresseName));
                    //email.Bcc.Add(new MailAddress(toAddress, addresseName));
                else
                    email.To.Add(new MailAddress(toAddress));
                    //email.Bcc.Add(new MailAddress(toAddress));

                email.Subject = subject;
                email.Body = msgBody;
                email.IsBodyHtml = isHtmlEmail;
                email.Priority = msgPriority;

                if (ccAddress != String.Empty)
                {
                    if (ccName != String.Empty)
                        email.CC.Add(new MailAddress(ccAddress, ccName));
                    else
                        email.CC.Add(new MailAddress(ccAddress));
                }

                client.Host = SmtpServer;
                client.Port = SmtpPort;
                client.EnableSsl = IsSSLRequired;


                if (IsAuthenticationRequired)
                {
                    NetworkCredential oCredential = new NetworkCredential(SmtpUserName, SmtpUserPassword);
                    client.UseDefaultCredentials = false;
                    client.Credentials = oCredential;
                }
                else
                {
                    client.UseDefaultCredentials = true;
                }
                client.Send(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
