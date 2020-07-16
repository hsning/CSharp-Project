using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Application
{
    public class EmailManager
    {
        public EmailManager()
        {

        }
        
        public void SendEmail(string emailTo,EmailType emailType,string token)
        {
            var appSettings = ConfigurationManager.AppSettings;
            MailMessage message=new MailMessage();
            if (emailType==EmailType.AccountVerification)
            {
                string tokenUrl = "/Activate?token=" + HttpUtility.UrlEncode(token);
                message = new MailMessage(appSettings["Email"], emailTo, "Email Verification","");
                message.IsBodyHtml = true;
                message.Body = "Please " + "<a href='https://" + HttpContext.Current.Request.Url.Authority + tokenUrl + "'>click here</a>" + " to verify your account";
            }
            if(emailType==EmailType.PasswordChange)
            {
                string tokenUrl = "/Activate?token=" + HttpUtility.UrlEncode(token);
                message = new MailMessage(appSettings["Email"], emailTo, "Password Reset", "");
                message.IsBodyHtml = true;
                message.Body = "Please " + "<a href='https://" + HttpContext.Current.Request.Url.Authority + tokenUrl + "'>click here</a>" + " to reset your password";
            }
            if(emailType==EmailType.EmailChange)
            {
                string tokenUrl = "/VerifyNewEmail?token=" + HttpUtility.UrlEncode(token);
                message = new MailMessage(appSettings["Email"], emailTo, "Change Email", "");
                message.IsBodyHtml = true;
                message.Body = "Please " + "<a href='https://" + HttpContext.Current.Request.Url.Authority + tokenUrl + "'>click here</a>" + " to reset your password";
            }
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(appSettings["Email"], appSettings["EmailPassword"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                    ex.ToString());
            }
        }
    }
}