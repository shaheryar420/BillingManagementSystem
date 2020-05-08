using BillingManagementSystem.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [SetPermissions]
        public ActionResult Index()
        {
            return View();
        }
    }
}