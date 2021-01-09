using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models.ResponseModels
{
    public class ConsummerPoolResponseModel
    {
        public string consummerId { get; set; }
        public string consummerNo { get; set; }
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string subAreaId { get; set; }
        public string areaId { get; set; }
        public string areaName { get; set; }
        public string subAreaName { get; set; }
        public string isActive { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}