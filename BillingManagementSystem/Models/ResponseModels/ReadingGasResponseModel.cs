﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ReadingGasResponseModel
    {
        public string readingGasId { get; set; }
        public string readingGasDatetime { get; set; }
        public string readingGasRemarks { get; set; }
        public string readingGasMeterNo { get; set; }
        public string readingGasPrevReading { get; set; }
        public string readingGasCurrentReading { get; set; }
        public string fk_readingPicture { get; set; }
        public string readingGasUnits { get; set; }
        public string readingGasAddedby { get; set; }
        public string readingGasMonth { get; set; }
        public string readingpicture_data { get; set; }
        public string readingpicture_type { get; set; }
        public string readingpicture_size { get; set; }
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