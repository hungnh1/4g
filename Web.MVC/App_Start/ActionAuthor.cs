using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Repository;
namespace baohiem.App_Start
{
    public class ActionAuthor : AuthorizeAttribute
    {

        private string _notifyUrl = "/Admin/UserRole/ErroPage";

        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
           

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (AuthorizeCore(filterContext.HttpContext))
            {
                //if (a.Check(filterContext.HttpContext.User.Identity.GetUserId(), controllerName, actionName))
                if (UserRepository.CheckAdminRole(filterContext.HttpContext.User.Identity.Name))
                {
                    HttpCachePolicyBase cachePolicy =
                       filterContext.HttpContext.Response.Cache;
                    cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                }
                else
                {
                    filterContext.Result = new RedirectResult(NotifyUrl);

                }
            }
            else
            {
                filterContext.Result = new RedirectResult(NotifyUrl);

            }

        }

    }
}