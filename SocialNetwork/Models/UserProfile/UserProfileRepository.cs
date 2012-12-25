using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class UserProfileRepository: BaseRepository 
    {

        public void SaveUserProfile(UserInfoModel userInfo)
        {
            Account account = _db.Accounts.SingleOrDefault(x => x.AccountID == userInfo.AccountID);

            account.FirstName = userInfo.FirstName;
            account.LastName = userInfo.LastName;
            account.Email = userInfo.Email;
            if (!String.IsNullOrEmpty(userInfo.City))
            {
                account.City = userInfo.City;
            }
            else
            {
                account.City = "";
            }

            if (!String.IsNullOrEmpty(userInfo.Country))
            {
                account.Country = userInfo.Country;
            }
            else
            {
                account.Country = "";
            }

            account.DateBirthday = userInfo.DateBirthday;

            _db.ApplyCurrentValues<Account>("Accounts", account);
            _db.SaveChanges();
        }

        public List<UserProfileModel> SearchAvailableFriendsForAccountID(int id, string searchString)
        {
            var excludeFriends = (from fr in _db.Friends
                                  where (fr.FirstAccountID == id)
                                  select fr.SecondAccountID).Union(from fr in _db.Friends
                                                                   where (fr.SecondAccountID == id)
                                                                   select fr.FirstAccountID);


            var allAccounts = from acc in _db.Accounts
                              where (acc.IsActivated == true) && (acc.AccountID != id) && (!excludeFriends.Contains(acc.AccountID))
                              select new
                              {
                                  acc.AccountID,
                                  acc.Username,
                                  acc.FirstName,
                                  acc.LastName,
                                  acc.Email,
                                  acc.City,
                                  acc.Country,
                                  acc.DateBirthday
                              };

            if (!String.IsNullOrEmpty(searchString))
            {
                allAccounts = from acc in allAccounts
                              where (acc.Username.ToUpper().Contains(searchString.ToUpper()) ||
                                     acc.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                                     acc.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                                     acc.Email.ToUpper().Contains(searchString.ToUpper()) ||
                                     acc.City.ToUpper().Contains(searchString.ToUpper()) ||
                                     acc.Country.ToUpper().Contains(searchString.ToUpper())
                                     )
                              select acc;
            }

            List<UserProfileModel> result = new List<UserProfileModel>();
            foreach (var account in allAccounts)
            {
                UserInfoModel userInfo = new UserInfoModel
                {
                    AccountID = account.AccountID,
                    Username = account.Username,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    City = account.City,
                    Country = account.Country,
                    DateBirthday = account.DateBirthday.GetValueOrDefault()
                };

                result.Add(new UserProfileModel
                {
                    UserInfo = userInfo
                });
            }

            return result;
        }

        public UserProfileModel GetUserProfileByAccountID(int accountID)
        {
            Account account = _db.Accounts.SingleOrDefault(x => x.AccountID == accountID);

            if (account == null)
            {
                return null;
            }

            UserInfoModel userInfo = new UserInfoModel
            {
                AccountID = account.AccountID,
                Username = account.Username,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                City = account.City,
                Country = account.Country,
                DateBirthday = account.DateBirthday.GetValueOrDefault()
            };

            UserProfileModel userProfile = new UserProfileModel
            {
                UserInfo = userInfo
            };

            FriendRepository fr = new FriendRepository();

            userProfile.Friendship = fr.GetAllFriendshipRecordByAccountID(accountID);

            return userProfile;
        }


    }
}