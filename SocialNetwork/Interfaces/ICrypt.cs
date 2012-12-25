using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Interfaces
{
    public interface ICrypt
    {
        string CreateSalt();
        string CreatePasswordHash(string password, string salt);
        string GeneratePassword();
    }
}
