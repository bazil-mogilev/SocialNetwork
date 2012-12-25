using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Models.Validation;

namespace SocialNetwork.Models
{
    public class UserInfoModel
    {
        
        [DisplayName("Account ID")]
        public int AccountID { get; set; }

        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [ValidateEmailAttribute]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Date birthday")]
        public DateTime DateBirthday { get; set; }
    }
}