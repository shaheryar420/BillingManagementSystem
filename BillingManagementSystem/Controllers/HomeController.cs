using BillingManagementSystem.App_Start;
using BillingManagementSystem.DataHelpers;
using BillingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoardStats()
        {
            DashBoardHelpers helper = new DashBoardHelpers();
            var response = helper.DashBoardStats();
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
    }
}