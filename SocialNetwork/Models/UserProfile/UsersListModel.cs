using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class UsersListModel
    {
        public List<UserProfileModel> Users;
        public FriendshipModel Friendship; 
    
        public UsersListModel()
        {
            Friendship = new FriendshipModel();
        }
    }
}