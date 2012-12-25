using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class FriendshipModel
    {
        public List<FriendsListModel> Friends;
        public List<FriendsListModel> RequestsFor;
        public List<FriendsListModel> RequestsFrom;
    }
}