using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingManagementSystem.Models;
using BillingManagementSystem.DataHelpers;
using System.Web.Http;

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
        #endregion
        #region Reading
        public ReadingElectricResponseModel AddReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.AddReadingElectric(model);
            return response;
        }
        public ReadingElectricResponseModel EditReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.EditReadingElectric(model);
            return response;
        }
        public ReadingElectricResponseModel DeleteReadingElectric([FromBody] ReadingElectricRequestModel model)
        {
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.DeleteReadingElectric(model);
            return response;
        }
        #endregion
    }
}