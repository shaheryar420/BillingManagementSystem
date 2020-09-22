using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ReadingElectricDetailByConsumerNoResponseModel
    {
        public string areaName { get; set; }
        public string subAreaName { get; set; }
        public string locationName { get; set; }
        public string locationId { get; set; }
        public string previousReading{ get; set; }
        public string resultCode { get; set; }
        public string remarks { get; set; }
    }
}