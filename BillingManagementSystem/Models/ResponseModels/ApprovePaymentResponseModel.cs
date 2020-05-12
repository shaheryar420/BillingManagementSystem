using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ApprovePaymentResponseModel
    {
        public string paymentId { get; set; }
        public string residentRank { get; set; }
        public string residentName { get; set; }
        public string residentPaNo { get; set; }
        public string meterNo { get; set; }
        public string paymentDatetime { get; set; }
        public string paymentAmount { get; set; }
        public string fk_paymentType { get; set; }
        public string fk_resident { get; set; }
        public string paymentMonth { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}