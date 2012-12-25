using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Serializable]
    public class InterlocutorModel
    {
        public int accountID { get; set; }
        public string Name { get; set; }
    }
}