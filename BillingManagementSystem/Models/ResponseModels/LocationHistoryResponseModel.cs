using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class LocationHistoryResponseModel
    {
        public string residentName { get; set; }
        public string paNo { get; set; }
        public string residentRank { get; set; }
        public string residentUnit { get; set; }
        public string residentStatus { get; set; }
        public string billingMonth { get; set; }
        public string billAmount { get; set; }
        public string OutstandingAmountOfMonth { get; set; }
        public string paymentMonth { get; set; }
        public string paymentAmount { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}