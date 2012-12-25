using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace SocialNetwork.Interfaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        bool PasswordResetEnabled { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email, string firstName, string lastName, bool isApproved);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool ResetPassword(string userName, string loginUrl);
    }
}
