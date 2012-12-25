using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class ErrorController : BaseLogonController
    {
        public ActionResult General(HandleErrorInfo he)
        {
            return View("General", he);
        }

        public ActionResult Http404()
        {
            return View("Http404");
        }

        public ActionResult Http403()
        {
            return View("Http403");
        }

        public ActionResult ExhibitNotFound()
        {
            return View();
            
        }

    }
}
