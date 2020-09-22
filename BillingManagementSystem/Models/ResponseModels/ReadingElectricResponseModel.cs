using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ReadingElectricResponseModel
    {
        public string readingElectricId { get; set; }
        public string readingElectricDatetime { get; set; }
        public string readingElectricRemarks { get; set; }
        public string readingElectricMeterNo { get; set; }
        public string readingElectricPrevReading { get; set; }
        public string readingElectricCurrentReading { get; set; }
        public string fk_readingPicture { get; set; }
        public string readingElectricUnits { get; set; }
        public string readingElectricAddedby { get; set; }
        public string readingElectricMonth { get; set; }
        public string readingpicture_data { get; set; }
        public string readingpicture_type { get; set; }
        public string readingpicture_size { get; set; }
        public string readingElectricTv { get; set; }
        public string readingElectricWater { get; set; }
        public string readingElectricFpa { get; set; }
        public string area_name { get; set; }
        public string area_id { get; set; }
        public string subarea_id { get; set; }
        public string subarea_name { get; set; }
        public string location_id { get; set; }
        public string location_name { get; set; }
        public string residentId { get; set; }
        public string residentName { get; set; }
        public string residentRank { get; set; }
        public string residentPANo { get; set; }
        public string residentUnit { get; set; }
        public string residentPinCode { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}