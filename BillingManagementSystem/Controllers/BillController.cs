using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingManagementSystem.Models;
using BillingManagementSystem.DataHelpers;
using System.Web.Http;
using System.IO;
using System.Drawing;
using BillingManagementSystem.App_Start;

namespace BillingManagementSystem.Controllers
{
    public class BillController : Controller
    {
        #region Views
        // GET: Bill
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
        public ActionResult BillList()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult AdminElectric()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult AdminGas()
        {
            return View();
        }
        #endregion
        #region  Electric Reading
        public ActionResult AddReadingElectric()
        {
            string _meterNo = Request.Form["_meterNo"].ToString();
            string _previousReading = Request.Form["_previousReading"].ToString();
            string _currentReading = Request.Form["_currentReading"].ToString();
            string _currentUnit = Request.Form["_currentUnit"].ToString();
            string _month = Request.Form["_month"].ToString();
            string userId = "1";
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

            ReadingElectricRequestModel model = new ReadingElectricRequestModel()
            {
                readingpicture_data = _base64,
                readingpicture_size = fileSize.ToString(),
                readingpicture_type = mimeType,
                readingElectricCurrentReading = _currentReading,
                readingElectricAddedby = userId,
                readingElectricPrevReading = _previousReading,
                readingElectricMonth = _month,
                readingElectricUnits = _currentUnit,
                readingElectricMeterNo = _meterNo
                
            };
            ReadingElectricHelpers helper = new ReadingElectricHelpers();
            var response = helper.AddReadingElectric(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditReadingElectric()
        {
            string _id = Request.Form["_id"].ToString();
            string _meterNo = Request.Form["_meterNo"].ToString();
            string _previousReading = Request.Form["_previousReading"].ToString();
            string _currentReading = Request.Form["_currentReading"].ToString();
            string _currentUnit = Request.Form["_currentUnit"].ToString();
            string _month = Request.Form["_month"].ToString();
            string userId = "1";
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

            ReadingElectricRequestModel model = new ReadingElectricRequestModel()
            {
                readingElectricId = _id,
                readingpicture_data = _base64,
                readingpicture_size = fileSize.ToString(),
                readingpicture_type = mimeType,
                readingElectricCurrentReading = _currentReading,
                readingElectricAddedby = userId,
                readingElectricPrevReading = _previousReading,
                readingElectricMonth = _month,
                readingElectricUnits = _currentUnit,
                readingElectricMeterNo = _meterNo

            };
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
        public ActionResult GetAllReadingsElectric()
        {
            string userId = Request.Cookies["bms_data"]["id"].ToString();
            ReadingElectricRequestModel model = new ReadingElectricRequestModel()
            {
                readingElectricAddedby = userId
            };
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
        #region Gas Reading
        public ActionResult AddReadingGas()
        {
            string _meterNo = Request.Form["_meterNo"].ToString();
            string _previousReading = Request.Form["_previousReading"].ToString();
            string _currentReading = Request.Form["_currentReading"].ToString();
            string _currentUnit = Request.Form["_currentUnit"].ToString();
            string _month = Request.Form["_month"].ToString();
            string userId = "1";
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

            ReadingGasRequestModel model = new ReadingGasRequestModel()
            {
                readingpicture_data = _base64,
                readingpicture_size = fileSize.ToString(),
                readingpicture_type = mimeType,
                readingGasCurrentReading = _currentReading,
                readingGasAddedby = userId,
                readingGasPrevReading = _previousReading,
                readingGasMonth = _month,
                readingGasUnits = _currentUnit,
                readingGasMeterNo = _meterNo

            };
            ReadingGasHelpers helper = new ReadingGasHelpers();
            var response = helper.AddReadingGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditReadingGas()
        {
            string _id = Request.Form["_id"].ToString();
            string _meterNo = Request.Form["_meterNo"].ToString();
            string _previousReading = Request.Form["_previousReading"].ToString();
            string _currentReading = Request.Form["_currentReading"].ToString();
            string _currentUnit = Request.Form["_currentUnit"].ToString();
            string _month = Request.Form["_month"].ToString();
            string userId = "1";
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

            ReadingGasRequestModel model = new ReadingGasRequestModel()
            {
                readingGasId = _id,
                readingpicture_data = _base64,
                readingpicture_size = fileSize.ToString(),
                readingpicture_type = mimeType,
                readingGasCurrentReading = _currentReading,
                readingGasAddedby = userId,
                readingGasPrevReading = _previousReading,
                readingGasMonth = _month,
                readingGasUnits = _currentUnit,
                readingGasMeterNo = _meterNo

            };
            ReadingGasHelpers helper = new ReadingGasHelpers();
            var response = helper.EditReadingGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteReadingGas([FromBody] ReadingGasRequestModel model)
        {
            ReadingGasHelpers helper = new ReadingGasHelpers();
            var response = helper.DeleteReadingGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllReadingsGas()
        {
            string userId = Request.Cookies["bms_data"]["id"].ToString();
            ReadingGasRequestModel model = new ReadingGasRequestModel()
            {
                readingGasAddedby = userId
            };
            ReadingGasHelpers helper = new ReadingGasHelpers();
            var response = helper.getAllReadings(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ApproveReadingGas([FromBody] ReadingGasRequestModel model)
        {
            string userId = Request.Cookies["bms_data"]["id"].ToString();
            model.userId = userId;

            ReadingGasHelpers helper = new ReadingGasHelpers();
            var response = helper.ApproveReadingGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
    }
}