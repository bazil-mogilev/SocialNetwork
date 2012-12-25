using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Interfaces;
using System.Web.Routing;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    public class ModeratorController : BaseLogonController
    {


        [Authorize(Roles = "Moderator")]
        public ActionResult Accounts(string currentFilter, string searchString)
        {
            AccountRepository _accountRepository = new AccountRepository();

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            return View(_accountRepository.GetAllAccounts(searchString));
        }

        
        [Authorize(Roles = "Moderator")]
        public ViewResult Edit(int id)
        {
            AccountRepository _accountRepository = new AccountRepository();
            return View(_accountRepository.GetAccountByID(id));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                AccountRepository _accountRepository = new AccountRepository();
                _accountRepository.SaveAccount(account);
            }
            return RedirectToAction("Accounts");
        }

        

        [Authorize(Roles = "Moderator")]
        public ViewResult DeleteAccount(int id)
        {
            AccountRepository _accountRepository = new AccountRepository();
            return View(_accountRepository.GetAccountByID(id));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("DeleteAccount")]
        public ActionResult DeleteAccountConfirmed(int id)
        {
            AccountRepository _accountrepository = new AccountRepository();
            _accountrepository.RemoveAccountByID(id);
            return RedirectToAction("Accounts");
        }

        

        [Authorize(Roles = "Moderator")]
        public ViewResult Dialogs()
        {
            DialogRepository _dialogRepository = new DialogRepository();

            return View(_dialogRepository.GetAllMessages());
        }

        
        [Authorize(Roles = "Moderator")]
        public ViewResult DeleteMessage(int id)
        {
            DialogRepository _dialogRepository = new DialogRepository();
            return View(_dialogRepository.GetMessageByID(id));
        }


        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("DeleteMessage")]
        public ActionResult DeleteMessageConfirmed(int id)
        {
            DialogRepository _dialogRepository = new DialogRepository();
            _dialogRepository.RemoveMessageByID(id);
            return RedirectToAction("Dialogs");
        }

        [Authorize(Roles = "Moderator")]
        public ViewResult Friends()
        {
            FriendRepository _friendRepository = new FriendRepository();

            return View(_friendRepository.GetAllFriendshipList());
        }

        [Authorize(Roles = "Moderator")]
        public ViewResult DeleteFriend(int firstID, int secondID)
        {
            FriendRepository _friendRepository = new FriendRepository();
            return View(_friendRepository.GetFriendship(firstID, secondID));
        }


        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("DeleteFriend")]
        public ActionResult DeleteFriendConfirmed(int firstID, int secondID)
        {
            FriendRepository _friendRepository = new FriendRepository();

            _friendRepository.RemoveFriendship(firstID,secondID);

            return RedirectToAction("Friends");
        }

    }
}
