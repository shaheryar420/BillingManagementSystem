using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillGasRequestModel
    {
        public string rank { get; set; }
        public string unit { get; set; }
        public string paNo { get; set; }
        public string areaId { get; set; }
        public string meterNo { get; set; }
        public string billGasId { get; set; }
        public string billGasDateTime { get; set; }
        public string billGasAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string fk_location { get; set; }
        public string fk_resident { get; set; }
        public string billGasPrevReading { get; set; }
        public string billGasCurrentReading { get; set; }
        public string billGasUnits { get; set; }
        public string billGasWater { get; set; }
        public string billGasTv { get; set; }
        public string billGasRemarks { get; set; }
        public string fk_billPicture { get; set; }
        public string billGasMonth { get; set; }
        public string billGasOutstanding { get; set; }
        public string residentName { get; set; }
    }
}