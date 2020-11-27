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
    public class RORController : Controller
    {
        #region views
        [SetPermissions]
        public ActionResult Electric()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult ApprovedReadings()
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

        #endregion
        #region Electric Actions
        public ActionResult GetAllRORElectric([FromBody] BillRequestModel model)
        {
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllROR(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GenerateROR([FromBody] RORRequestModel model)
        {
            RORHelpers helper = new RORHelpers();
            var response = helper.GenerateRor(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
    
        #endregion
       
    }
}