using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Interfaces;
using System.Web.Routing;
using SocialNetwork.Services;
using SocialNetwork.Models;
using SocialNetwork.Components;

namespace SocialNetwork.Controllers
{
    public class BaseLogonController: Controller 
    {
        protected CurrentUser _user;

        public int CurrentAccountID
        {
            get
            {
                GetCurrentUserInfo();
                return _user.AccountID;
            }
        }

        private void GetCurrentUserInfo()
        {
            if (User != null)
                {
                    AccountRepository accountRepository = new AccountRepository();
                    _user.Username = User.Identity.Name;
                    _user.AccountID = accountRepository.GetAccountIDByUserName(_user.Username);
                    _user.IsModerator = User.IsInRole("Moderator");
                }
        }

        public BaseLogonController()
        {
            if (_user == null)
            {
                _user = new CurrentUser();
            }
        }


        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var action = filterContext.Result as ViewResult;
            if (action != null)
            {
                action.ViewData["accountID"] = CurrentAccountID;
            }
        }

    }
}