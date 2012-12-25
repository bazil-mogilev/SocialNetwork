using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SocialNetwork.Services;
using SocialNetwork.Interfaces;
using System.Configuration;
using SocialNetwork.Controllers;

namespace SocialNetwork
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 "Activate",
                 "Account/Activate/{username}/{key}",
             new
             {
                 controller = "Account",
                 action = "Activate",
                 username = UrlParameter.Optional,
                 key = UrlParameter.Optional
             });


            routes.MapRoute(
                "Users", // Route name
                "Users/{id}/{action}", // URL with parameters
                new
                {
                    controller = "UserProfile",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                "Dialogs", // Route name
                "Dialogs/{id}/{action}", // URL with parameters
                new
                {
                    controller = "Dialog",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                "Friends", // Route name
                "Friends/{id}/{action}", // URL with parameters
                new
                {
                    controller = "Friend",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
            );

          
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {

            HttpContext ctx = HttpContext.Current;
            Exception ex = ctx.Server.GetLastError();
            ctx.Response.Clear();

            RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
            BaseLogonController controller = new ErrorController(); 
            var context = new ControllerContext(rc, (ControllerBase)controller);

            var viewResult = new ViewResult();

            var httpException = ex as HttpException;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 403:
                        viewResult.ViewName = "Http403";
                        break;

                    case 404:
                        viewResult.ViewName = "Http404";
                        break;

                    default:
                        viewResult.ViewName = "General";
                        break;
                }
            }
            else
            {
                viewResult.ViewName = "General";
            }
            
            viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
            
            viewResult.ViewData["AccountID"] = controller.CurrentAccountID;
            viewResult.ExecuteResult(context);

            ctx.Server.ClearError();

        }
       
    }
}