using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using System.Web.Security;
using System.Security.Cryptography;

namespace SocialNetwork.Components
{
    public class Crypt:ICrypt
    {
        public string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        public string GeneratePassword()
        {
            return Membership.GeneratePassword(8,0);
        }
    }
}