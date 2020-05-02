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
    public class ManagementController : Controller
    {
        // GET: Management
        #region Views
        public ActionResult FixedRates()
        {
            return View();
        }
        #endregion
        #region Actions
        #region User
        public UserResponseModel AddUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.AddUser(model);
            return response;
        }
        public UserResponseModel EditUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.EditUser(model);
            return response;
        }
        public UserResponseModel DeleteUser([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.DeleteUser(model);
            return response;
        }
        public UserResponseModel GetUserById([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetUserById(model);
            return response;
        }
        public List<UserResponseModel> GetAllUsers([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetAllUsers(model);
            return response;
        }
        public List<UserResponseModel> GetAllUsersByUserType([FromBody] UserRequestModel model)
        {
            UserHelpers helper = new UserHelpers();
            var response = helper.GetAllUsersByUserType(model);
            return response;
        }
        #endregion
        #region Location
        public LocationResponseModel AddLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.AddLocation(model);
            return response;
        }
        public LocationResponseModel EditLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.EditLocation(model);
            return response;
        }
        public LocationResponseModel DeleteLocation([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.DeleteLocation(model);
            return response;
        }
        public LocationResponseModel GetLocationById([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetLocationById(model);
            return response;
        }
        public List<LocationResponseModel> GetAllLocations([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetAllLocations(model);
            return response;
        }
        public List<LocationResponseModel> GetAllLocationsByArea([FromBody] LocationRequestModel model)
        {
            LocationHelpers helper = new LocationHelpers();
            var response = helper.GetAllLocationsByArea(model);
            return response;
        }
        #endregion
        #region Area
        public AreaResponseModel AddArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.AddArea(model);
            return response;
        }
        public AreaResponseModel EditArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.EditArea(model);
            return response;
        }
        public AreaResponseModel DeleteArea([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.DeleteArea(model);
            return response;
        }
        public AreaResponseModel GetAreaById([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.GetAreaById(model);
            return response;
        }
        public List<AreaResponseModel> GetAllAreas([FromBody] AreaRequestModel model)
        {
            AreaHelpers helper = new AreaHelpers();
            var response = helper.GetAllAreas(model);
            return response;
        }
        
        #endregion
        #endregion
    }
}