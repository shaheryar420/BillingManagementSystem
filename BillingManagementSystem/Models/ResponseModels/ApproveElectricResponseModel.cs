using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ApproveElectricResponseModel
    {
        public string billElectricAmount { get; set; }
        public string billElectricPrevReading { get; set; }
        public string billElectricCurrentReading { get; set; }
        public string billElectricUnits { get; set; }
        public string billFurnitureCharges { get; set; }
        public string billElectricWater { get; set; }
        public string billElectricTv { get; set; }
        public string billElectricRemarks { get; set; }
        public string billElectricMonth { get; set; }
        public string billElectricOutstanding { get; set; }
        public string subAreaName { get; set; }
        public string areaName { get; set; }
        public string energyCharges { get; set; }
        public string locationName { get; set; }
        public string locationMeterNo { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }

    }
}