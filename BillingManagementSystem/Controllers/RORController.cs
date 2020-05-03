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
    public class RORController : Controller
    {
        #region views
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
        #region Actions
        public ActionResult GetAllRORElectric([FromBody] BillElectricRequestModel model)
        {
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByMonth([FromBody] BillElectricRequestModel model)
        {
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByMonth(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #endregion
    }
}