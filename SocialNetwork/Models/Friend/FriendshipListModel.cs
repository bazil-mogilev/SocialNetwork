using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SocialNetwork.Models
{
    public class FriendshipListModel
    {
        [DisplayName("First ID")]
        public int FirstID { get; set; }

        [DisplayName("First username")]
        public string FirstUsername { get; set; }

        [DisplayName("Second ID")]
        public int SecondID { get; set; }

        [DisplayName("Second username")]
        public string SecondUsername { get; set; }

        [DisplayName("Is friend")]
        public bool IsFriend { get; set; }

        [DisplayName("Request from")]
        public string RequestFrom { get; set; }
    }
}