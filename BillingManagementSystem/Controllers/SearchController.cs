using BillingManagementSystem.App_Start;
using BillingManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [SetPermissions]
        public ActionResult SearchBill()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult SearchResident()
        {
            return View();
        }

        public ActionResult ResidentProfile(int id)
        {
            ResidentViewModel model = new ResidentViewModel();
            model.resident_id = id.ToString();
            return View(model);
        }
    }
}