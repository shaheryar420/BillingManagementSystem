using BillingManagementSystem.App_Start;
using BillingManagementSystem.DataHelpers;
using BillingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult RecoveryApprovalGas()
        {
            return View();
        }
        #region Electric
        public ActionResult AddPayment()
        {
            string _amount = Request.Form["_amount"].ToString();
            string _residentId = Request.Form["_residentId"].ToString();
            string _month = Request.Form["_month"].ToString();
            HttpPostedFileBase file = Request.Files[0]; //Uploaded file
                                                        //Use the following properties to get file's name, size and MIMEType
            int fileSize = file.ContentLength;
            string fileName = file.FileName;
            string mimeType = file.ContentType;
            System.IO.Stream fileContent = file.InputStream;
            string ext = Path.GetExtension(fileName);
            byte[] thePictureAsBytes = new byte[fileSize];
            using (BinaryReader theReader = new BinaryReader(fileContent))
            {
                thePictureAsBytes = theReader.ReadBytes(fileSize);
            }

            String _base64 = Convert.ToBase64String(thePictureAsBytes);
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
        public ActionResult EditPayment(PaymentRequestModel model)
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.EditPayment(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region Gas
        public ActionResult AddPaymentGas()
        {
            string _amount = Request.Form["_amount"].ToString();
            string _residentId = Request.Form["_residentId"].ToString();
            string _month = Request.Form["_month"].ToString();
            HttpPostedFileBase file = Request.Files[0]; //Uploaded file
                                                        //Use the following properties to get file's name, size and MIMEType
            int fileSize = file.ContentLength;
            string fileName = file.FileName;
            string mimeType = file.ContentType;
            System.IO.Stream fileContent = file.InputStream;
            string ext = Path.GetExtension(fileName);
            byte[] thePictureAsBytes = new byte[fileSize];
            using (BinaryReader theReader = new BinaryReader(fileContent))
            {
                thePictureAsBytes = theReader.ReadBytes(fileSize);
            }

            String _base64 = Convert.ToBase64String(thePictureAsBytes);
            PaymentRequestModel model = new PaymentRequestModel()
            {

                paymentAmount = _amount,
                residentId = _residentId,
                paymentMonth = _month

            };
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.AddPaymentGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetAllGasPayments()
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.GetAllGasPayments();
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ApproveGasPayment(PaymentRequestModel model)
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.ApproveGasPayment(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditGasPayment(PaymentRequestModel model)
        {
            RecoveryHelpers helper = new RecoveryHelpers();
            var response = helper.EditGasPayment(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
    }
}