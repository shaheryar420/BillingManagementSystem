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
        public string paymentAmount { get; set; }
        public string fk_paymentType { get; set; }
        public string paymentMonth { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}