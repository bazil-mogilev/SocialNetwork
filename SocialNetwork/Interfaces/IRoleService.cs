using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Interfaces
{
    public interface IRoleService
    {
        bool ModeratorExists();
        void AddUsersToRoles(string[] usernames, string[] rolenames);
        void RemoveUsersFromRoles(string[] usernames, string[] rolenames);
        void CreateRole(string roleName);
    }
}
