using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using System.Web.Security;
using SocialNetwork.Models;
using SocialNetwork.Components;

namespace SocialNetwork.Services
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        private IEventMessageGenerator _messageGenerator = new EventMessageGenerator();

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool PasswordResetEnabled
        {
            get
            {
                return _provider.EnablePasswordReset;
            }
        }

        public bool ResetPassword(string userName, string loginUrl)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            MembershipUser user = _provider.GetUser(userName, false);
            if (user == null)
            {
                return false;
            }
            else
            {
                string newPassword = user.ResetPassword();
                if (newPassword == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, string firstName, string lastName, bool isApproved)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(firstName)) throw new ArgumentException("Value cannot be null or empty.", "firstName");
            if (String.IsNullOrEmpty(lastName)) throw new ArgumentException("Value cannot be null or empty.", "lastName");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, isApproved, null, out status);

            if (status == MembershipCreateStatus.Success)
            {
                AccountRepository accountRepository = new AccountRepository();
                accountRepository.RegisterUserProfile(userName, firstName, lastName);
            }

            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            try
            {
                if (ValidateUser(userName, oldPassword))
                {
                    MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                    if (currentUser.ChangePassword(oldPassword, newPassword))
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

    }
}