using BillingManagementSystem.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BillingManagementSystem.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SetPermissionsAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Do some logic here to pull authorised roles from backing store (AppSettings, MSSQL, MySQL, MongoDB etc)
            bool isUserAuthorized = false;

            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            // Check that the user belongs to one or more of these roles 
            if (httpContext.Request.Cookies["bms_data"] != null)
            {
                var routeData = httpContext.Request.RequestContext.RouteData;
                var controller = routeData.GetRequiredString("controller");
                var action = routeData.GetRequiredString("action");
                string userId = httpContext.Request.Cookies["bms_data"]["id"];
                var permissions = new UserHelpers().GetUserPermissions(userId);
                var actionList = permissions.Select(x => x.controller + "/" + x.action).ToList();
                if (actionList.Contains(controller + "/" + action))
                {
                    isUserAuthorized = true;
                }

                if (isUserAuthorized)
                    return true;

            }
            return base.AuthorizeCore(httpContext);
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // If they are authorized, handle accordingly
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                if(filterContext.RequestContext.HttpContext.Request.Cookies["bms_data"] != null)
                {
                    if(filterContext.RequestContext.HttpContext.Request.Cookies["bms_data"]["Id"] != null)
                    {
                        filterContext.Result = new RedirectResult("~/Home/Index");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Login/Login");
                    }
                    
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                }
            }
        }
    }
}