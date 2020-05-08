using BillingManagementSystem.App_Start;
using BillingManagementSystem.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class RecoveryController : Controller
    {
        [SetPermissions]
        public ActionResult Electric()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Gas()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Wapda()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult RecoveryApproval()
        {
            return View();
        }
        
    }
}