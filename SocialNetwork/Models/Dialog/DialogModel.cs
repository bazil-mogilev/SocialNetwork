using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SocialNetwork.Models
{
    [Serializable]
    public class DialogModel: IComparable<DialogModel>
    {
        [DisplayName("ID")]
        public int MessageID { get; set; }

        [Required]
        [DisplayName("Sender ID")]
        public int SenderID { get; set; }

        [DisplayName("Sender")]
        public string SenderUsername { get; set; } 

        [Required]
        [DisplayName("Recepient ID")]
        public int RecepientID { get; set; }

        [DisplayName("Recepient")]
        public string RecepientUsername { get; set; }

        [DisplayName("Subject")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Message")]
        public string MessageText { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Time")]
        public DateTime DateTime { get; set; }

        public bool IsMyMessage { get; set; }

        public int CompareTo(DialogModel other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.DateTime.CompareTo(other.DateTime);

        }
    }
}