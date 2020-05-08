using BillingManagementSystem.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BillingManagementSystem
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active-link";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            if (controller == currentController)
            {
                cssClass = "active";
                if (action == currentAction)
                {
                    cssClass = "active-link";
                }
            }
            else if (action == currentAction)
            {
                cssClass = "active-link";
            }
            else
            {
                cssClass = String.Empty;
            }
            
            return cssClass;
        }
        public static string IsAllowed(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (action != null)
            {
                string role = System.Web.HttpContext.Current.Request.Cookies["bms_data"]["Id"];
                var permissions = new UserHelpers().GetUserPermissions(role);
                var actionList = permissions.Select(x => x.controller + "/" + x.action).ToList();
                if (!actionList.Contains(controller + "/" + action))
                {
                    cssClass = "hidden";
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Cookies["bms_data"] != null)
                {
                    string role = System.Web.HttpContext.Current.Request.Cookies["bms_data"]["Id"];
                    var permissions = new UserHelpers().GetUserPermissions(role);
                    var actionList = permissions.Select(x => x.controller).ToList();
                    if (!actionList.Contains(controller))
                    {
                        cssClass = "hidden";
                    }
                }
                else
                {
                    cssClass = "hidden";
                }

            }
            return cssClass;
        }
        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
