using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Interfaces;
using SocialNetwork.Services;
using System.Web.Routing;

namespace SocialNetwork.Controllers
{
    public class UserProfileController : BaseLogonController
    {
        // GET: /UserProfile/id/Index/
        [Authorize]
        public ViewResult Index(int id)
        {
            UserProfileRepository userprofileRepository = new UserProfileRepository();
            var userProfile = userprofileRepository.GetUserProfileByAccountID(id);

            if (userProfile == null)
            {
                //redirect error
                return View("Http404");
             }
            
            return View("Index",userProfile);
        }

        // GET: /UserProfile/id/Edit/
        [Authorize]
        public ViewResult Edit(int id)
        {

            UserProfileRepository userprofileRepository = new UserProfileRepository();
            UserProfileModel userProfile;

            if ((id == CurrentAccountID) || (User.IsInRole("Moderator")))
            {
                userProfile = userprofileRepository.GetUserProfileByAccountID(id);
            }
            else
            {
                userProfile = userprofileRepository.GetUserProfileByAccountID(CurrentAccountID);
            }

            if (userProfile == null)
            {
                //redirect error
                RedirectToAction("Http404");
            }

            return View("Edit",userProfile.UserInfo);
        }

        // POST: /UserProfile/id/Edit/
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserInfoModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfileRepository userprofileRepository = new UserProfileRepository();
                userprofileRepository.SaveUserProfile(model);
                
                return RedirectToAction("Index", model.AccountID);
            }
            else
            {
                ModelState.AddModelError("","User profile saving was unsuccessful. Please correct the errors and try again.");
            }

            return View("Edit",model);
        }

        

    }
}
