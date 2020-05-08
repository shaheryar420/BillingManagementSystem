using BillingManagementSystem.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BillingManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if(Request.Cookies["bms_data"]!=null)
            {
                Response.Cookies["bms_data"].Expires = DateTime.UtcNow.AddHours(5).AddHours(-1);
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginRequest(string username, string password)
        {
            LoginHelpers helper = new LoginHelpers();
            var response = helper.LoginUser(username, password);
            if (response.resultCode == "1100")
            {
                HttpCookie cookie = new HttpCookie("bms_data");
                cookie.Values.Add("id", response.userId);
                cookie.Values.Add("val1", response.userFullName);
                cookie.Values.Add("val2", response.userName);
                cookie.Values.Add("val3", response.userType);
                cookie.Values.Add("val4", response.userTypeId);
                Response.Cookies.Add(cookie);
            }
            var json = Json(response);
            return json;
        }

        [HttpPost]
        public ActionResult UpdatePassword(string oldpassword, string newpassword)
        {

            return Json("success");
        }
    }
}