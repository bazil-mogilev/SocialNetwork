using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using SocialNetwork.Components;
using System.Configuration;

namespace SocialNetwork.Models
{
    public class FriendRepository: BaseRepository 
    {
        public List<FriendsListModel> GetFriendsByAccountID(int id)
        {
            var rightFriends = from fr in _db.Friends
                               join acc in _db.Accounts
                               on fr.SecondAccountID equals acc.AccountID
                               where ((fr.FirstAccountID == id) && (fr.IsFriend == true))
                               select new { acc.AccountID, acc.Username, acc.FirstName, acc.LastName, fr.IsFriend };

            var leftFriends = from fr in _db.Friends
                              join acc in _db.Accounts
                              on fr.FirstAccountID equals acc.AccountID
                              where ((fr.SecondAccountID == id) && (fr.IsFriend == true))
                              select new { acc.AccountID, acc.Username, acc.FirstName, acc.LastName, fr.IsFriend };

            var allFriends = rightFriends.Union(leftFriends);

            List<FriendsListModel> result = new List<FriendsListModel>();
            foreach (var fr in allFriends)
            {
                result.Add(new FriendsListModel 
                    { 
                        AccountID = id,
                        FriendID = fr.AccountID,
                        UserName = fr.Username,
                        FirstName = fr.FirstName,
                        LastName = fr.LastName,
                        IsFriend = fr.IsFriend,
                        IsMyRequest = false
                    });
            }

            return result;
        }

        public List<FriendsListModel> GetFriendshipRequestFromAccount(int id)
        {
            var requests = from fr in _db.Friends
                                join acc in _db.Accounts
                                on fr.SecondAccountID equals acc.AccountID
                                where ((fr.FirstAccountID == id) && (fr.IsFriend == false))
                                select new { acc.AccountID, acc.Username, acc.FirstName, acc.LastName, fr.IsFriend };

            List<FriendsListModel> result = new List<FriendsListModel>();
            foreach (var fr in requests)
            {
                result.Add(new FriendsListModel
                {
                    AccountID = id,
                    FriendID = fr.AccountID,
                    UserName = fr.Username,
                    FirstName = fr.FirstName,
                    LastName = fr.LastName,
                    IsFriend = fr.IsFriend,
                    IsMyRequest = true
                });
            }

            return result;
        }

        public List<FriendsListModel> GetFriendshipRequestForAccount(int id)
        {
            var requests = from fr in _db.Friends
                           join acc in _db.Accounts
                           on fr.FirstAccountID equals acc.AccountID
                           where ((fr.SecondAccountID == id) && (fr.IsFriend == false))
                           select new { acc.AccountID, acc.Username, acc.FirstName, acc.LastName, fr.IsFriend };

            List<FriendsListModel> result = new List<FriendsListModel>();
            foreach (var fr in requests)
            {
                result.Add(new FriendsListModel
                {
                    AccountID = id,
                    FriendID = fr.AccountID,
                    UserName = fr.Username,
                    FirstName = fr.FirstName,
                    LastName = fr.LastName,
                    IsFriend = fr.IsFriend,
                    IsMyRequest = false
                });
            }

            return result;
        }

        protected Friend GetFriendshipRecord(int firstAccountID, int secondAccountID)
        {
            Friend friendship = _db.Friends.SingleOrDefault(x => (x.FirstAccountID == firstAccountID && x.SecondAccountID == secondAccountID));

            if (friendship == null)
            {
                friendship = _db.Friends.SingleOrDefault(x => (x.FirstAccountID == secondAccountID && x.SecondAccountID == firstAccountID));
            }

            return friendship;
        }

        public void AcceptFriendshipRequest(int currentAccountID, int friendAccountID)
        {
            Friend friendshipRecord = GetFriendshipRecord(currentAccountID, friendAccountID);

            if (friendshipRecord != null)
            {
                friendshipRecord.IsFriend = true;
                _db.ApplyCurrentValues<Friend>("Friends", friendshipRecord);
                _db.SaveChanges();

                _notifyService.SendMessage(
                            GetEmailByAccountID(friendAccountID), 
                            "You have new friend", 
                            _messageGenerator.MessageAcceptFriendshipRequest(
                                GetFirstLastNameByAccountID(currentAccountID),
                                ConfigurationManager.AppSettings.Get("base_application_url") + "Friends/"+ friendAccountID.ToString()));
            }
        }

        public void RemoveFriendship(int currentAccountID, int friendAccountID)
        {
            Friend friendshipRecord = GetFriendshipRecord(currentAccountID, friendAccountID);

            if (friendshipRecord != null)
            {
                _db.Friends.DeleteObject(friendshipRecord);
                _db.SaveChanges();

                _notifyService.SendMessage(
                            GetEmailByAccountID(friendAccountID),
                            "You lost friend",
                            _messageGenerator.MessageDeniedFriendshipRequest(
                                GetFirstLastNameByAccountID(currentAccountID),
                                ConfigurationManager.AppSettings.Get("base_application_url") + "Friends/" + friendAccountID.ToString()));
            }
        }

        public FriendshipListModel GetFriendship(int firstAccountID, int secondAccountID)
        {
            var friendship= (from fr in _db.Friends
                           
                            join first in _db.Accounts
                            on fr.FirstAccountID equals first.AccountID

                            join second in _db.Accounts
                            on fr.SecondAccountID equals second.AccountID
                            
                            where (fr.FirstAccountID == firstAccountID) && (fr.SecondAccountID ==secondAccountID)

                            select new FriendshipListModel {
                                FirstID = fr.FirstAccountID, 
                                FirstUsername = first.Username, 
                                SecondID = fr.SecondAccountID, 
                                SecondUsername= second.Username, 
                                IsFriend = fr.IsFriend,
                                RequestFrom = first.Username
                                }
                            );

            return friendship.FirstOrDefault();
        }

        public void SendFriendshipRequest(int currentAccountID, int friendAccountID)
        {
            Friend friendshipRecord = GetFriendshipRecord(currentAccountID, friendAccountID);

            if (friendshipRecord == null)
            {
                friendshipRecord = new Friend { FirstAccountID = currentAccountID, SecondAccountID = friendAccountID, IsFriend = false };
                _db.AddToFriends(friendshipRecord);
                _db.SaveChanges();

                _notifyService.SendMessage(
                                            GetEmailByAccountID(friendAccountID),
                                            "You have new friend request",
                                            _messageGenerator.MessageSendFriendshipRequest(
                                                    GetFirstLastNameByAccountID(currentAccountID),
                                                    ConfigurationManager.AppSettings.Get("base_application_url") + "Friends/" + friendAccountID.ToString()));
             }
        }

        public FriendshipModel GetAllFriendshipRecordByAccountID(int id)
        {
            FriendshipModel result = new FriendshipModel();

            result.Friends = GetFriendsByAccountID(id);
            result.RequestsFor = GetFriendshipRequestForAccount(id);
            result.RequestsFrom = GetFriendshipRequestFromAccount(id);

            return result;
        }

        public List<InterlocutorModel> GetInterlocutors(int id)
        {
            var friends = GetFriendsByAccountID(id);

            List<InterlocutorModel> result = new List<InterlocutorModel>();
            foreach (var fr in friends)
            {
                result.Add(new InterlocutorModel
                {
                    accountID = fr.AccountID,
                    Name = fr.UserName
                });
            }
            return result;
        }

        public List<FriendshipListModel> GetAllFriendshipList()
        {
            var rightFriends = from fr in _db.Friends
                               join acc in _db.Accounts
                               on fr.SecondAccountID equals acc.AccountID
                               select new { acc.AccountID, acc.Username, acc.FirstName, acc.LastName, fr.IsFriend };

            var friendship = (from fr in _db.Friends

                                  join first in _db.Accounts
                                  on fr.FirstAccountID equals first.AccountID

                                  join second in _db.Accounts
                                  on fr.SecondAccountID equals second.AccountID

                                  select new FriendshipListModel()
                                  {
                                      FirstID = fr.FirstAccountID,
                                      FirstUsername = first.Username,
                                      SecondID = fr.SecondAccountID,
                                      SecondUsername = second.Username,
                                      IsFriend = fr.IsFriend,
                                      RequestFrom = first.Username
                                  }
                                  );
            return friendship.ToList();

        }
    }
}