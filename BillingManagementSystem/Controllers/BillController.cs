using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult Electric()
        {
            return View();
        }
        public ActionResult Gas()
        {
            return View();
        }
        public ActionResult Wapda()
        {
            return View();
        }
        public ActionResult BillList()
        {
            return View();
        }
    }
}