using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillGasResponseModel
    {
        public string billGasId { get; set; }
        public string billGasDateTime { get; set; }
        public string billGasAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string paymentStatusName { get; set; }
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
        public string remarks { get; set; }
        public string resultCode { get; set; }
        public string pictureData { get; set; }
        public string pictureSize { get; set; }
        public string pictureType { get; set; }
        public string residentName { get; set; }
        public string residentPaNo { get; set; }
        public string residentRank { get; set; }
        public string residentRemarks { get; set; }
        public string residentUnit { get; set; }
        public string locationName { get; set; }
        public string locationMeterNo { get; set; }
    }
}