using BillingManagementSystem.App_Start;
using BillingManagementSystem.DataHelpers;
using BillingManagementSystem.Models;
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
        public ActionResult AddPayment()
        {
            string _amount = Request.Form["_amount"].ToString();
            string _residentId = Request.Form["_residentId"].ToString();
            string _month = Request.Form["_month"].ToString();

            PaymentRequestModel model = new PaymentRequestModel()
            {
                
                paymentAmount = _amount,
                residentId = _residentId,
                paymentMonth = _month

            };
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.AddPayment(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetAllPayments()
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.GetAllPayments();
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ApproveElectricPayment(PaymentRequestModel model)
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.ApproveElectricPayment(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
    }
}