using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ApproveGasResponseModel
    {
        public string billGasAmount { get; set; }
        public string locationName { get; set; }
        public string locationMeterNo { get; set; }
        public string billGasPrevReading { get; set; }
        public string billGasCurrentReading { get; set; }
        public string billGasUnits { get; set; }
        public string billGasWater { get; set; }
        public string billGasTv { get; set; }
        public string billGasRemarks { get; set; }
        public string billGasMonth { get; set; }
        public string billGasOutstanding { get; set; }
        public string subAreaName { get; set; }
        public string areaName { get; set; }
        public string gasCharges { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}