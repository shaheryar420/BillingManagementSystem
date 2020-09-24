using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class LocationRequestModel
    {
        public string userId { get; set; }
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string fk_subArea { get; set; }
        public string locationElectricMeter { get; set; }
        public string locationGassMeter { get; set; }
        public string locationWapdaMeter { get; set; }
        public string locationTvStatus { get; set; }
        public string locationWaterStatus { get; set; }
        public string locationSecondMeter { get; set; }
    }
}