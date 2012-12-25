using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using SocialNetwork.Interfaces;
using System.Configuration;

namespace SocialNetwork.Components
{
    public class Email : INotifyService
    {
        private static SmtpClient CreateSMTPClient()
        {
          return  new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings.Get("smtp_host"),
                Port = int.Parse(ConfigurationManager.AppSettings.Get("smtp_port")),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000,
                Credentials = new System.Net.NetworkCredential( ConfigurationManager.AppSettings.Get("smtp_mail"),
                                                                ConfigurationManager.AppSettings.Get("smtp_password")),
                EnableSsl = true
            };
        }

        public void SendMessage(string to, string subject, string body)
        {
            // Instantiate a new instance of MailMessage 
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message 
            mMailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("smtp_mail"));
            // Set the recepient address of the mail message 
            mMailMessage.To.Add(new MailAddress(to));

            mMailMessage.Subject = subject;
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML 
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal 
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient 
            SmtpClient mSmtpClient = CreateSMTPClient();

            // Send the mail message 
            mSmtpClient.Send(mMailMessage);
        }

    }
}