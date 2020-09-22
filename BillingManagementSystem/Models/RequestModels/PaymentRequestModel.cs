using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class PaymentRequestModel
    {
        public string readingpicture_data { get; set; }
        public string readingpicture_type { get; set; }
        public string readingpicture_size { get; set; }
        public string billingMonth { get; set; }
        public string residentId { get; set; }
        public string paymenthistoryId { get; set; }
        public string paymenthistoryDatetime { get; set; }
        public string paymentAmount { get; set; }
        public string fk_paymentType { get; set; }
        public string paymentMonth { get; set; }
    }
}