using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Components
{
    public class EventMessageGenerator: IEventMessageGenerator
    {
        public string MessageActivateAccount(string userName, string activateUrl)
        {
            string message = string.Format("Welcom {0}!\r\n", userName);
            message += "Thank you for registering!";
            message += "\r\n";
            message += string.Format("To activate account and login go to: {0}\r\n", activateUrl);

            return message;
        }

        public string MessageNewPassword(string userName, string password, string loginUrl)
        {
            string message = string.Format("Your user name is {0}\r\n", userName);
            message += string.Format("Your password is {0}\r\n", password);
            message += "\r\n";

            if (String.IsNullOrEmpty(loginUrl))
            {
                message += string.Format("To login go to {0}\r\n", loginUrl);
            }

            return message;
        }

        public string MessageSendFriendshipRequest(string friendName, string siteUrl)
        {
            string message = string.Format("{0} would like to add you to friends.\r\n", friendName);
            message += "\r\n";

            if (String.IsNullOrEmpty(siteUrl))
            {
                message += string.Format("To see all friends go to {0}\r\n", siteUrl);
            }

            return message;
        }

        public string MessageAcceptFriendshipRequest(string friendName, string siteUrl)
        {
            string message = string.Format("{0} to accept your request to friends.\r\n", friendName);
            message += "\r\n";

            if (String.IsNullOrEmpty(siteUrl))
            {
                message += string.Format("To see all friends go to {0}\r\n", siteUrl);
            }

            return message;
        }

        public string MessageDeniedFriendshipRequest(string friendName, string siteUrl)
        {
            string message = string.Format("{0} to denied your request to friends.\r\n", friendName);
            message += "\r\n";

            if (String.IsNullOrEmpty(siteUrl))
            {
                message += string.Format("To see all friends go to {0}\r\n", siteUrl);
            }

            return message;
        }
    }
}