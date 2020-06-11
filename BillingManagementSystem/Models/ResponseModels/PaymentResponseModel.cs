using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class PaymentResponseModel
    {
        public string paymenthistoryId { get; set; }
        public string paymenthistoryDatetime { get; set; }
        public string residentName { get; set; }
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string subAreaId { get; set; }
        public string subAreaName { get; set; }
        public string areaId { get; set; }
        public string areaName { get; set; }
        public string outstanding { get; set; }
        public string paymentAmount { get; set; }
        public string fk_readingPicture { get; set; }
        public string fk_paymentType { get; set; }
        public string readingpicture_data { get; set; }
        public string readingpicture_type { get; set; }
        public string readingpicture_size { get; set; }
        public string paymentMonth { get; set; }
        public string pictureId { get; set; }
        public string picturedata { get; set; }
        public string pictureSize { get; set; }
        public string pictureType { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}