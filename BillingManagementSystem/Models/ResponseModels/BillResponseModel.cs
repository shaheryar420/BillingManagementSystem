using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillResponseModel
    {
        public string billId { get; set; }
        public string billDateTime { get; set; }
        public string billAmount { get; set; }
        public string fk_paymentStatus { get; set; }
        public string paymentStatusName { get; set; }
        public string fk_location { get; set; }
        public string fk_resident { get; set; }
        public string billPrimaryPrevReading { get; set; }
        public string billPrimaryCurrentReading { get; set; }
        public string billSecondaryPrevReading { get; set; }
        public string billSecondaryCurrentReading { get; set; }
        public string billGasPrevReading { get; set; }
        public string billGasCurrentReading { get; set; }
        public string billPrimaryAmount { get; set; }
        public string billSecondaryAmount { get; set; }
        public string billGasAmount { get; set; }
        public string billPrimaryUnits { get; set; }
        public string billPrimaryWater { get; set; }
        public string billPrimaryTv { get; set; }
        public string billPrimaryRebate { get; set; }
        public string billPrimaryFPA { get; set; }
        public string billPrimaryMeterRent { get; set; }
        public string billPrimaryPictureData { get; set; }
        public string billPrimaryPictureSize { get; set; }
        public string billPrimaryPictureType { get; set; }
        public string billPrimaryFurCharges { get; set; }
        public string billSecondaryPictureData { get; set; }
        public string billSecondaryPictureSize { get; set; }
        public string billSecondaryPictureType { get; set; }
        public string billSecondaryFurCharges { get; set; }
        public string billGasPictureId { get; set; }
        public string billPrimaryPictureId { get; set; }
        public string billSecondaryPictureId { get; set; }
        public string billGasPictureData { get; set; }
        public string billGasPictureSize { get; set; }
        public string billGasPictureType { get; set; }
        public string billSecondaryUnits { get; set; }
        public string billGasMMBTU { get; set; }
        public string billSecondaryWater { get; set; }
        public string billSecondaryTv { get; set; }
        public string billSecondaryRebate { get; set; }
        public string billSecondaryFPA { get; set; }
        public string billSecondaryMeterRent { get; set; }
        public string billGasMeterRent { get; set; }
        public string billGasGST { get; set; }
        public string billMonth { get; set; }
        public string billOutstanding { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
        public string residentName { get; set; }
        public string residentPaNo { get; set; }
        public string residentRank { get; set; }
        public string residentRemarks { get; set; }
        public string residentUnit { get; set; }
        public string locationName { get; set; }
        public string locationMeterNo { get; set; }
        public string payment { get; set; }
        public string paymentDate { get; set; }
        public string paymentMonth { get; set; }
        public string areaId { get; set; }
        public string areaName { get; set; }
    }
}