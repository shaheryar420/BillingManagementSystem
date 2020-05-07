using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingManagementSystem.Models;
using BillingManagementSystem.DataHelpers;
using System.Web.Http;
using System.IO;

namespace BillingManagementSystem.Controllers
{
    public class BillController : Controller
    {
        #region Views
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
        public ActionResult AdminElectric()
        {
            return View();
        }
        #endregion
        #region Reading
        public ActionResult AddReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            

            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.AddReadingElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.EditReadingElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.DeleteReadingElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllReadingsElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.getAllReadings(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ApproveReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.ApproveReadingElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
    }
}