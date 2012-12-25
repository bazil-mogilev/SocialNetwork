using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using System.Web.Security;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string username, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(username)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}