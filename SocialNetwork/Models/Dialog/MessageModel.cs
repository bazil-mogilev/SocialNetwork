using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SocialNetwork.Models
{
    [Serializable]
    public class MessageModel
    {
        [DisplayName("Subject")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Message")]
        public string MessageText { get; set; }

        public MessageModel()
        {
            Subject = "";
            MessageText = "";
        }
    }
}