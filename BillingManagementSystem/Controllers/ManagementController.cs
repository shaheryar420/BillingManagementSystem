using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult FixedRates()
        {
            return View();
        }
    }
}