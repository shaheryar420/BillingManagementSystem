using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class LocationRequestModel
    {
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string fk_area { get; set; }
        public string locationElectricMeter { get; set; }
        public string locationGassMeter { get; set; }
        public string locationWapdaMeter { get; set; }
    }
}