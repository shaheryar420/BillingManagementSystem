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
            return View();
        }

        [HttpPost]
        public ActionResult LoginRequest(string username, string password)
        {
            return Json("success");
        }

        [HttpPost]
        public ActionResult UpdatePassword(string oldpassword, string newpassword)
        {

            return Json("success");
        }
    }
}