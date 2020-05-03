using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillElectricRequestModel
    {
        public string rank { get; set; }
        public string unit { get; set; }
        public string paNo { get; set; }
        public string areaName { get; set; }
        public string meterNo { get; set; }
        public string billElectricId { get; set; }
        public string billElectricDateTime { get; set; }
        public string billElectricAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string fk_location { get; set; }
        public string fk_resident { get; set; }
        public string billElectricPrevReading { get; set; }
        public string billElectricCurrentReading { get; set; }
        public string billElectricUnits { get; set; }
        public string billElectricWater { get; set; }
        public string billElectricTv { get; set; }
        public string billElectricRemarks { get; set; }
        public string fk_billPicture { get; set; }
        public string billElectricMonth { get; set; }
        public string billElectricOutstanding { get; set; }
    }
}