using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Interfaces
{
    public interface IEventMessageGenerator
    {
        string MessageActivateAccount(string userName, string activateUrl);
        string MessageNewPassword(string userName, string password, string loginUrl);
        string MessageSendFriendshipRequest(string friendName, string siteUrl);
        string MessageAcceptFriendshipRequest(string friendName, string siteUrl);
        string MessageDeniedFriendshipRequest(string friendName, string siteUrl);
    }
}
