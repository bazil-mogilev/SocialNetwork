using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class FriendController : BaseLogonController
    {
        // POST: /UserProfile/id/Friend/
        [Authorize]
        public ActionResult Index(int id, string currentFilter, string searchString)
        {
            UserProfileRepository userprofileRepository = new UserProfileRepository();
            FriendRepository friendRepository = new FriendRepository();

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["AccountID"] = CurrentAccountID;

            return View("Index", new UsersListModel
            {
                Friendship = friendRepository.GetAllFriendshipRecordByAccountID(id),
                Users = userprofileRepository.SearchAvailableFriendsForAccountID(id, searchString)
            });
        }

        // GET: /Userprofile/id/DeleteFriend/id
        [Authorize]
        public ViewResult DeleteFriend(int id)
        {
            UserProfileRepository userprofileRepository = new UserProfileRepository();

            return View("DeleteFriend", userprofileRepository.GetUserProfileByAccountID(id).UserInfo);
        }

        // POST: /UserProfile/id/Friend/
        [Authorize]
        [HttpPost, ActionName("DeleteFriend")]
        public ActionResult DeleteFriendConfirmed(int id)
        {
            FriendRepository friendRepository = new FriendRepository();
            friendRepository.RemoveFriendship(CurrentAccountID, id);

            return RedirectToAction("Index", "Friend", new { id = CurrentAccountID });
        }

        // GET: /Userprofile/id/DeleteFriend/id
        [Authorize]
        public ViewResult AcceptFriend(int id)
        {
            UserProfileRepository userprofileRepository = new UserProfileRepository();

            return View("AcceptFriend", userprofileRepository.GetUserProfileByAccountID(id).UserInfo);
        }

        [Authorize]
        [HttpPost, ActionName("AcceptFriend")]
        public ActionResult AcceptFriendConfirmed(int id)
        {
            FriendRepository friendRepository = new FriendRepository();
            friendRepository.AcceptFriendshipRequest(CurrentAccountID, id);

            return RedirectToAction("Index", "Friend", new { id = CurrentAccountID });
        }

        [Authorize]
        public ViewResult SendRequest(int id)
        {
            UserProfileRepository userprofileRepository = new UserProfileRepository();

            return View("SendRequest", userprofileRepository.GetUserProfileByAccountID(id).UserInfo);
        }

        [Authorize]
        [HttpPost, ActionName("SendRequest")]
        public ActionResult SendRequestConfirmed(int id)
        {
            FriendRepository friendRepository = new FriendRepository();
            friendRepository.SendFriendshipRequest(CurrentAccountID, id);

            
            return RedirectToAction("Index","Friend", new { id = CurrentAccountID });
        }
    }
}
