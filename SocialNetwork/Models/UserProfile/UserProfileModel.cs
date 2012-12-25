using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Models.Validation;

namespace SocialNetwork.Models
{
    public class UserProfileModel
    {

        public UserInfoModel UserInfo;

        public FriendshipModel Friendship;

        public UserProfileModel()
        {
            UserInfo = new UserInfoModel();
            Friendship = new FriendshipModel();
        }
        
    }
}