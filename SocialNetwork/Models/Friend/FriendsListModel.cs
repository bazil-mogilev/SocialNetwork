using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Models
{
    public class FriendsListModel
    {
        [DisplayName("Account ID")]
        public int AccountID { get; set; }

        [DisplayName("Friend ID")]
        public int FriendID { get; set; }

        [DisplayName("Friend name")]
        public string UserName { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Is friend")]
        public bool IsFriend { get; set; }

        public bool IsMyRequest { get; set; }

        
    }
}