using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Components;

namespace SocialNetwork.Controllers
{
    public class DialogController : BaseLogonController
    {
        protected string _dataKey = "_convModel";
        protected ConversationModel _convModel;

        // GET: /Users/id/Dialog/
        [Authorize]
        public ActionResult Index(int id)
        {
            DialogRepository dialogRepository = new DialogRepository();

            if (id == CurrentAccountID)
            {
                List<InterlocutorModel> interlocutorsList = dialogRepository.GetInterlocutors(CurrentAccountID);
                if (interlocutorsList.Count == 0)
                {
                    return RedirectToAction("Index", "Friend", CurrentAccountID);
                }
                else
                {
                    _convModel.InterlocutorID = interlocutorsList.First().accountID;
                }
            }
            else
            {
                _convModel.InterlocutorID = id;
            }
        
            _convModel.CurrentUserID = CurrentAccountID;

            _convModel.InterlocutorUsername = dialogRepository.GetInterlocutorNameByID(_convModel.InterlocutorID);
            _convModel.Dialogs = dialogRepository.GetMessagesForDialog(_convModel.CurrentUserID, _convModel.InterlocutorID);
            _convModel.Interlocutors = dialogRepository.GetInterlocutors(_convModel.CurrentUserID);

            return View("Index", _convModel);
        }

        [Authorize]
        [HttpPost, ActionName("Index")]
        public ActionResult SendMessage(MessageModel model)
        {
            if (ModelState.IsValid)
            {
                DialogRepository dialogRepository = new DialogRepository();

                dialogRepository.SendMessage(new DialogModel
                {
                    SenderID = _convModel.CurrentUserID,
                    RecepientID = _convModel.InterlocutorID,
                    DateTime = DateTime.Now,
                    Subject = model.Subject,
                    MessageText = model.MessageText
                });
            }
            else
            {
                ModelState.AddModelError("", "Message was not sent.");
            }

            return RedirectToAction("Index", _convModel.InterlocutorID);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _convModel = (SerializationHelper.Deserialize(Request.Form[_dataKey])
                                                        ?? TempData[_dataKey]
                                                        ?? new ConversationModel()) as ConversationModel;

            TryUpdateModel(_convModel);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
            {
                TempData[_dataKey] = _convModel;
            }
            else
            {
                if (filterContext.Result is ViewResult)
                {
                    ViewData[_dataKey] = SerializationHelper.Serialize(_convModel);
                    ViewData["accountID"] = _convModel.CurrentUserID;
                }
            }
        }

    }
}
