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
        public ActionResult SearchBill()
        {
            return View();
        }
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