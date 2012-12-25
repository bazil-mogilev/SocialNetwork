using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Serializable]
    public class CurrentUser
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public bool IsModerator { get; set; }
    }
}