using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingManagementSystem.Models;
using BillingManagementSystem.DataHelpers;
using System.Web.Http;
using BillingManagementSystem.App_Start;

namespace BillingManagementSystem.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        #region Views
        [SetPermissions]
        public ActionResult FixedRates()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Users()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Area()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Location()
        {
            return View();
        }
        [SetPermissions]
        public ActionResult Resident()
        {
            return View();
        }

        #endregion
        #region Actions
        #region User
        public ActionResult AddUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.AddUser(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.EditUser(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.DeleteUser(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetUserById([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetUserById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllUsers([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetAllUsers(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllUsersByUserType([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetAllUsersByUserType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region UserType
        public ActionResult AddUserType([FromBody] UserTypeRequestModel model)
        {
            UserTypeHelpers helper = new UserTypeHelpers();
            var response = helper.AddUserType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditUserType([FromBody] UserTypeRequestModel model)
        {
            UserTypeHelpers helper = new UserTypeHelpers();
            var response = helper.EditUserType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteUserType([FromBody] UserTypeRequestModel model)
        {
            UserTypeHelpers helper = new UserTypeHelpers();
            var response = helper.DeleteUserType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetUserTypeById([FromBody] UserTypeRequestModel model)
        {
            UserTypeHelpers helper = new UserTypeHelpers();
            var response = helper.GetUserTypeById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllUserTypes([FromBody] UserTypeRequestModel model)
        {
            UserTypeHelpers helper = new UserTypeHelpers();
            var response = helper.GetAllUserTypes(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        
        #endregion
        #region Location
        public ActionResult AddLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.AddLocation(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.EditLocation(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.DeleteLocation(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetLocationById([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetLocationById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllLocations([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetAllLocations(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllLocationsByArea([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetAllLocationsByArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region Area
        public ActionResult AddArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.AddArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.EditArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.DeleteArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAreaById([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.GetAreaById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllAreas()
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.GetAllAreas(new AreaRequestModel());
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllAreasByUser()
        {
            string userId = Request.Cookies["bms_data"]["id"].ToString();
            AreaRequestModel model = new AreaRequestModel()
            {
                userId = userId
            };
            AreaHelpers helper = new AreaHelpers();
            var response = helper.GetAllAreasByUser(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        #endregion
        #region Resident
        public ActionResult AddResident([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.AddResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditResident([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.EditResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteResident([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.DeleteResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetResidentById([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetResidentById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidents([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidents(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsForSearch([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsForSearch(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsForSearchByPaNo([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsForSearchByPaNo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsForSearchByUnit([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsForSearchByUnit(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsForSearchByRank([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsForSearchByRank(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsByArea([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsByArea(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult SelectDetailInfoByResident([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.SelectDetailInfoByResident(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllResidentsDetailInfo([FromBody] ResidentRequestModel model)
        {
            ResidentHelpers helper = new ResidentHelpers();
            var response = helper.GetAllResidentsDetailInfo(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region Fixed Rate
        public ActionResult EditFixedRate([FromBody] FixedRatesRequestModel model)
        {
            FixedRatesHelpers helper = new FixedRatesHelpers();
            var response = helper.UpdateFixedRates(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetFixedRateByName([FromBody] FixedRatesRequestModel model)
        {
            string userId = Request.Cookies["bms_data"]["id"].ToString();
            model.userId = userId;
            FixedRatesHelpers helper = new FixedRatesHelpers();
            var response = helper.GetFixedRatesByName(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion
        #region Fixed Rate Type
        public ActionResult AddFixedRateType([FromBody] FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeHelpers helper = new FixedRatesTypeHelpers();
            var response = helper.AddFixedRateType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult EditFixedRateType([FromBody] FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeHelpers helper = new FixedRatesTypeHelpers();
            var response = helper.EditFixedRateType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult DeleteFixedRateType([FromBody] FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeHelpers helper = new FixedRatesTypeHelpers();
            var response = helper.DeleteFixedRateType(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetFixedRateTypeById([FromBody] FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeHelpers helper = new FixedRatesTypeHelpers();
            var response = helper.GetFixedRateTypeById(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult GetAllFixedRateTypes([FromBody] FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeHelpers helper = new FixedRatesTypeHelpers();
            var response = helper.GetAllFixedRateTypes(model);
            var json = Json(response);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        #endregion
        #endregion
    }
}