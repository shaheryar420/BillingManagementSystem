using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class LocationResponseModel
    {
        public string residentId { get; set; }
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string fk_area { get; set; }
        public string locationElectricMeter { get; set; }
        public string locationGassMeter { get; set; }
        public string locationWapdaMeter { get; set; }
        public string previousReading { get; set; }
        public string outstanding { get; set; }
        public string billMonth { get; set; }
        public string currentReading { get; set; }
        public string currentUnit { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}