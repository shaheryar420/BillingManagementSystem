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
            model.billElectricMonth = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByMonth(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByAmount([FromBody] BillElectricRequestModel model)
        {
            model.billElectricAmount = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByAmount(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByMeterNo([FromBody] BillElectricRequestModel model)
        {
            model.meterNo = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByMeterNo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByArea([FromBody] BillElectricRequestModel model)
        {
            model.areaid = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByResident([FromBody] BillElectricRequestModel model)
        {
            model.residentName = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByPaNo([FromBody] BillElectricRequestModel model)
        {
            model.paNo = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByPaNo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByUnit([FromBody] BillElectricRequestModel model)
        {
            model.unit = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByUnit(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByRank([FromBody] BillElectricRequestModel model)
        {
            model.rank = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByRank(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORElectricByDateEntered([FromBody] BillElectricRequestModel model)
        {
            model.billElectricDateTime = model.billElectricId;
            model.billElectricId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORElectricByDate(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region Gas Actions
        public ActionResult GetAllRORGas([FromBody] BillGasRequestModel model)
        {
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllGas(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByMonth([FromBody] BillGasRequestModel model)
        {
            model.billGasMonth = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllGasByMonth(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByAmount([FromBody] BillGasRequestModel model)
        {
            model.billGasAmount = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllGasByAmount(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByMeterNo([FromBody] BillGasRequestModel model)
        {
            model.meterNo = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllGasByMeterNo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByArea([FromBody] BillGasRequestModel model)
        {
            model.areaId = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByResident([FromBody] BillGasRequestModel model)
        {
            model.residentName = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByPaNo([FromBody] BillGasRequestModel model)
        {
            model.paNo = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByPaNo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByUnit([FromBody] BillGasRequestModel model)
        {
            model.unit = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByUnit(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByRank([FromBody] BillGasRequestModel model)
        {
            model.rank = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByRank(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllRORGasByDateEntered([FromBody] BillGasRequestModel model)
        {
            model.billGasDateTime = model.billGasId;
            model.billGasId = null;
            RORHelpers helper = new RORHelpers();
            var response = helper.GetAllRORGasByDate(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
    }
}