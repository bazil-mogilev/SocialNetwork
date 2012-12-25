using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using System.Web.Security;

namespace SocialNetwork.Services
{
    public class AccountRoleService : IRoleService
    {
        private readonly RoleProvider _provider;

        public AccountRoleService()
            : this(null)
        {
        }

        public AccountRoleService(RoleProvider provider)
        {
            _provider = provider ?? new SNRoleProvider();
        }

        public void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            _provider.AddUsersToRoles(usernames, rolenames);
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            _provider.RemoveUsersFromRoles(usernames, rolenames);
        }

        public void CreateRole(string roleName)
        {
            _provider.CreateRole(roleName);
        }

        public bool ModeratorExists()
        {
            var users = _provider.GetUsersInRole("Moderator");

            return users.Count() != 0;
        }
    }

}