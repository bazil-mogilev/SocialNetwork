using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using SocialNetwork.Models;

public class SNRoleProvider : RoleProvider
{
    private string _applicationName;
    private AccountRepository _accountRepository = new AccountRepository();


    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
            throw new ArgumentNullException("config");

        if (name == null || name.Length == 0)
            name = "CustomRoleProvider";

        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Custom Role Provider");
        }

        base.Initialize(name, config);

        _applicationName = GetConfigValue(config["applicationName"],
                      System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

    }

    private string GetConfigValue(string configValue, string defaultValue)
    {
        if (string.IsNullOrEmpty(configValue))
            return defaultValue;

        return configValue;
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        _accountRepository.AddUsersToRoles(usernames, roleNames);
    }

    public override string ApplicationName
    {
        get 
        { 
            return _applicationName; 
        }
        set 
        { 
            _applicationName = value; 
        }
    }

    public override void CreateRole(string roleName)
    {
        _accountRepository.CreatePermission(roleName);
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
        throw new NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
        throw new NotImplementedException();
    }

    public override string[] GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public override string[] GetRolesForUser(string username)
    {
        var account = _accountRepository.GetAccountByUserName(username);
        return account.Permissions
            .Select(x => x.Name).ToArray();
    }

    public override string[] GetUsersInRole(string permissionName)
    {
        var permission = _accountRepository.GetPermissionByName(permissionName);
        var accountnames = _accountRepository.GetAllAccounts("")
            .Where(x => x.Permissions.Contains(permission))
            .Select(x => x.Username);

        return accountnames.ToArray();
    }

    public override bool IsUserInRole(string username, string roleName)
    {
       var roles = GetRolesForUser(username);
       return roles.Contains(roleName);
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
        throw new NotImplementedException();
    }

    public override bool RoleExists(string roleName)
    {
        throw new NotImplementedException();
    }
}
