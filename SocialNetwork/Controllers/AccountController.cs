using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SocialNetwork.Models;
using SocialNetwork.Interfaces;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{

    [HandleError]
    public class AccountController : BaseLogonController
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        public IRoleService RoleService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            if (RoleService == null) { RoleService = new AccountRoleService(); }

            base.Initialize(requestContext);
        }
       


        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            if (!RoleService.ModeratorExists())
            {
                TempData["Message"] = "Moderators are not found. Create moderator!";
                return RedirectToAction("ModeratorSetup");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //route moderator to moderatorpage
                        AccountRepository _db = new AccountRepository();

                        if (Roles.IsUserInRole(model.UserName,"Moderator"))
                        {
                                                        
                            return RedirectToAction("Accounts", "Moderator");
                        }
                        else
                        {
                            return RedirectToAction("Index", "UserProfile", new { id = _db.GetAccountIDByUserName(model.UserName) });
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************
        [Authorize]
        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser( model.UserName, model.Password, model.Email, 
                                                                                    model.FirstName, model.LastName, false);

                if (createStatus == MembershipCreateStatus.Success)
                {
                                       
                    return RedirectToAction("Welcome","Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        // **************************************
        // URL: /Account/Activate/username/key
        // **************************************

        public ActionResult Activate(string username, string key)
        {
            AccountRepository _db = new AccountRepository();
            if (_db.ActivateUser(username, key) == false)
            {
                return RedirectToAction("Edit", "UserProfile", new { id = _db.GetAccountIDByUserName(username) });
            }
            else
            {
                return RedirectToAction("LogOn");
            }
        }

        // **************************************
        // URL: /Account/ModeratorSetup
        // **************************************
        public ActionResult ModeratorSetup()
        {
            if (RoleService.ModeratorExists())
                return RedirectToAction("LogOn");

            return View();
        }

        [HttpPost]
        public ActionResult ModeratorSetup(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email, model.FirstName, model.LastName, true);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    RoleService.CreateRole("Moderator");
                    RoleService.AddUsersToRoles(new string[] { model.UserName }, new string[] { "Moderator" });
                    FormsService.SignIn(model.UserName, true);
                    return RedirectToAction("Accounts", "Moderator");
                }

                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            return View(model);
        }


        // **************************************
        // URL: /Account/PasswordReset
        // **************************************
        public ActionResult PasswordReset()
        {
            if (!MembershipService.PasswordResetEnabled) throw new Exception("Password reset is not allowed\r\n");
            return View();
        }

        // **************************************
        // URL: /Account/PasswordReset
        // **************************************
        [HttpPost]
        public ActionResult PasswordReset(string userName)
        {
            if (!MembershipService.PasswordResetEnabled) throw new Exception("Password reset is not allowed\r\n");

            if (ModelState.IsValid)
            {
                    if (MembershipService.ResetPassword(userName, GetLoginUrl()))
                    {
                        return RedirectToAction("PasswordResetFinal", new { userName = userName });
                    }
                    else
                    {
                        ModelState.AddModelError("", "User is not found.");
                    }
            }
            return View();
        }

        // **************************************
        // URL: /Account/PasswordResetFinal
        // **************************************
        public ActionResult PasswordResetFinal(string userName)
        {
            if (!MembershipService.PasswordResetEnabled) throw new Exception("Password reset is not allowed\r\n");
            return View();
        }

        private string GetLoginUrl()
        {
            string thisUrl = Request.Url.AbsoluteUri;
            string baseUrl = thisUrl.Substring(0, thisUrl.LastIndexOf('/'));
            return baseUrl + "/LogOn";
        }

    }
}
