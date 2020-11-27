using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillRequestModel
    {
        public string rank { get; set; }
        public string unit { get; set; }
        public string paNo { get; set; }
        public string areaid { get; set; }
        public string meterNo { get; set; }
        public string billId { get; set; }
        public string billDateTime { get; set; }
        public string billAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string fk_location { get; set; }
        public string fk_resident { get; set; }
        public string residentName { get; set; }
        public string billPrevReading { get; set; }
        public string billCurrentReading { get; set; }
        public string billUnits { get; set; }
        public string billWater { get; set; }
        public string billTv { get; set; }
        public string billRemarks { get; set; }
        public string fk_billPicture { get; set; }
        public string billElectricMonth { get; set; }
        public string billElectricOutstanding { get; set; }
    }
}