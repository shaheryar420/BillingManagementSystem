using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class FixedRatesResponseModel
    {
        public string fixedRatesId { get; set; }
        public string fixedRatesName { get; set; }
        public string fixedRatesAmount { get; set; }
        public string fk_type { get; set; }
        public string fixedRatesUnit { get; set; }
        public string fixedRatesStatus { get; set; }
        public string resultCode { get; set; }
        public string remarks { get; set; }
    }
}