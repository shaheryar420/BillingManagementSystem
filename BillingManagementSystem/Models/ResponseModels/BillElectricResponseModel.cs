using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillElectricResponseModel
    {
        public string billElectricId { get; set; }
        public string billElectricDateTime { get; set; }
        public string billElectricAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string paymentStatusName { get; set; }
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
        public string billElectricFpa { get; set; }
        public string billElectricRebate { get; set; }
        public string payment { get; set; }
        public string paymentDate { get; set; }
        public string paymentMonth { get; set; }
    }
}