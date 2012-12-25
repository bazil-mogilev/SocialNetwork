using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models.Validation
{
    public class ValidateUsernameAttribute : RegularExpressionAttribute
    {
        public ValidateUsernameAttribute()
            : base(@"[A-Za-z][A-Za-z0-9._]{2,14}")
        {
            this.ErrorMessage = "Not a valid username. Username must be at least 3 characters long.";
        }
    }
}