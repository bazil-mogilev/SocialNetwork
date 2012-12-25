using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using SocialNetwork.Components;
using SocialNetwork.Interfaces;
using System.Configuration;

namespace SocialNetwork.Models
{
    public class AccountRepository: BaseRepository
    {
        private ICrypt _crypt = new Crypt();

        public MembershipUser CreateMembershipUser(string username, string password, string email, bool isApproved)
	    {
	            Account account = new Account();

                account.Username = username;
                account.Email = email;
                account.PasswordSalt = _crypt.CreateSalt();
                account.Password = _crypt.CreatePasswordHash(password, account.PasswordSalt);
                account.CreatedDate = DateTime.Now;

                if (!isApproved)
                {
                    account.IsActivated = false;
                    account.NewEmailKey = GenerateKey();
                }
                else
                    account.IsActivated = true;

                account.IsLockedOut = false;
                account.LastLockedOutDate = DateTime.Now;
                account.LastLoginDate = DateTime.Now;
                account.LastModifiedDate = DateTime.Now;

                account.DateBirthday = DateTime.Now;

                _db.AddToAccounts(account);
                _db.SaveChanges();

                if (!isApproved)
                {
                    string ActivationLink = ConfigurationManager.AppSettings.Get("base_application_url") + "Account/Activate/" + account.Username + "/" + account.NewEmailKey;

                    _notifyService.SendMessage(account.Email, "Actiovate your account!", _messageGenerator.MessageActivateAccount(account.Username, ActivationLink));
                }

                return GetMembershipUser(username);
        }

        

        private static string GenerateKey()
        {
            Guid emailKey = Guid.NewGuid();

            return emailKey.ToString();
        }

        public bool ActivateUser(string username, string key)
        {
                var result = from acc in _db.Accounts where (acc.Username == username) select acc;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    if (dbuser.NewEmailKey == key)
                    {
                        dbuser.IsActivated = true;
                        dbuser.LastModifiedDate = DateTime.Now;
                        dbuser.NewEmailKey = null;

                        _db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            
        }

        public string ResetPassword(string username)
        {
            var result = from acc in _db.Accounts where (acc.Username == username) select acc;
            string pass = null;

            if (result.Count() != 0)
            {
                var dbaccount = result.First();

                pass = _crypt.GeneratePassword();

                dbaccount.PasswordSalt = _crypt.CreateSalt();
                dbaccount.Password = _crypt.CreatePasswordHash(pass, dbaccount.PasswordSalt);

                SaveAccount(dbaccount);

                _notifyService.SendMessage(dbaccount.Email, "Reset password", _messageGenerator.MessageNewPassword(dbaccount.Username, pass, ConfigurationManager.AppSettings.Get("base_application_url") + "Logon"));
            }

            return pass;

        }

        public bool ChangePassword(string username, string pass)
        {
            var result = from acc in _db.Accounts where (acc.Username == username) select acc;
    
            if (result.Count() != 0)
            {
                var dbaccount = result.First();

                dbaccount.PasswordSalt = _crypt.CreateSalt();
                dbaccount.Password = _crypt.CreatePasswordHash(pass, dbaccount.PasswordSalt);

                SaveAccount(dbaccount);

                _notifyService.SendMessage(dbaccount.Email, "Change password", _messageGenerator.MessageNewPassword(dbaccount.Username, pass, ConfigurationManager.AppSettings.Get("base_application_url") + "Logon"));
            
                return true;
            }

            return false;

        }

        public bool ValidateUser(string username, string password)
        {
                var result = from acc in _db.Accounts where (acc.Username == username) select acc;

                if (result.Count() != 0)
                {
                    var dbaccount = result.First();

                    Crypt crypt = new Crypt();

                    if (dbaccount.Password == crypt.CreatePasswordHash(password, dbaccount.PasswordSalt) &&
	                                                       dbaccount.IsActivated == true)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            
        }


        public string GetUserNameByEmail(string email)
        {
                var result = from acc in _db.Accounts where (acc.Email == email) select acc;
                if (result.Count() != 0)
                {
                    var dbaccount = result.FirstOrDefault();
                    return dbaccount.Username;
                }
                else
                {
                    return "";
                }
        }

        public int GetAccountIDByUserName(string username)
        {
            var result = from acc in _db.Accounts where (acc.Username == username) select acc;
            if (result.Count() != 0)
            {
                var dbaccount = result.FirstOrDefault();
                return dbaccount.AccountID;
            }
            else
            {
                return 0;
            }
        }

        public MembershipUser GetMembershipUser(string username)
        {
            var result = from acc in _db.Accounts where (acc.Username == username) select acc;
            if (result.Count() != 0)
            {
                var dbaccount = result.FirstOrDefault();

                string _username = dbaccount.Username;
                int _providerUserKey = dbaccount.AccountID;
                string _email = dbaccount.Email;
                string _passwordQuestion = "";
                string _comment = dbaccount.Comments;
                bool _isApproved = dbaccount.IsActivated;
                bool _isLockedOut = dbaccount.IsLockedOut;
                DateTime _creationDate = dbaccount.CreatedDate;
                DateTime _lastLoginDate = dbaccount.LastLoginDate;
                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Now;
                DateTime _lastLockedOutDate = dbaccount.LastLockedOutDate;

                MembershipUser user = new MembershipUser("CustomMembershipProvider",
                                                        _username,
                                                        _providerUserKey,
                                                        _email,
                                                        _passwordQuestion,
                                                        _comment,
                                                        _isApproved,
                                                        _isLockedOut,
                                                        _creationDate,
                                                        _lastLoginDate,
                                                        _lastActivityDate,
                                                        _lastPasswordChangedDate,
                                                        _lastLockedOutDate);

                return user;
            }
            else
            {
                return null;
            }
        }

        public Account GetAccountByUserName(string username)
        {
            return _db.Accounts.SingleOrDefault(x => x.Username == username);
        }

        public Account GetAccountByID(int accountID)
        {
            return _db.Accounts.SingleOrDefault(x => x.AccountID == accountID);
        }

        public Permission GetPermissionByName(string name)
        {
            return _db.Permissions.SingleOrDefault(x => x.Name == name);
        }

        public List<Account> GetAllAccounts(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return (from acc in _db.Accounts
                        where (acc.Username.ToUpper().Contains(searchString.ToUpper()) ||
                               acc.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                               acc.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                               acc.Email.ToUpper().Contains(searchString.ToUpper())
                               )
                        select acc).ToList();
            }
            
            return _db.Accounts.ToList();
        }

        public void AddUsersToRoles(string[] usernames, string[] permissionnames)
        {
            foreach (var username in usernames)
            {
                var account = GetAccountByUserName(username);
                if (account != null)
                {
                    foreach (var rolename in permissionnames)
                    {
                        var permission = GetPermissionByName(rolename);
                        if (permission != null)
                            if (!account.Permissions.Contains(permission))
                                account.Permissions.Add(permission);
                    }
                }
            }
            _db.SaveChanges();
        }

        public void CreatePermission(string permissionName)
        {
            if (GetPermissionByName(permissionName) == null)
            {
                _db.AddToPermissions(new Permission { Name = permissionName });
                _db.SaveChanges();
            }
        }

        public void RemoveAccountByID(int id)
        {
            var account = GetAccountByID(id);

            if (account != null)
            {
                _db.ExecuteStoreCommand("DELETE FROM Friend WHERE (FirstAccountID = {0}) OR (SecondAccountID = {1})", id,id);
                _db.ExecuteStoreCommand("DELETE FROM Message WHERE (SenderAccountID = {0}) OR (RecepientAccountID = {1})", id, id);
                _db.ExecuteStoreCommand("DELETE FROM AccountPermission WHERE (Account = {0})", id);
                _db.Accounts.DeleteObject(account);
                _db.SaveChanges();
            }
        }

        public void SaveAccount(Account account)
        {
            Account acc = GetAccountByID(account.AccountID);
            acc.City = account.City;
            acc.Country = account.Country;
            acc.DateBirthday = account.DateBirthday;
            acc.FirstName = account.FirstName;
            acc.LastName = account.LastName;
            acc.Email = account.Email;
            acc.LastModifiedDate = DateTime.Now;
            
            _db.ApplyCurrentValues<Account>("Accounts", acc);
            _db.SaveChanges();
        }

        public void RegisterUserProfile(string username, string firstName, string lastName)
        {
            Account acc = GetAccountByUserName(username);
            acc.FirstName = firstName;
            acc.LastName  = lastName;
            acc.City = "";
            acc.Country = "";
            acc.DateBirthday = DateTime.Now;
            
            _db.ApplyCurrentValues<Account>("Accounts", acc);
            _db.SaveChanges();
        }
    }
}