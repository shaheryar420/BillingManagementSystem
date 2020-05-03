using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class FixedRatesTypeResponseModel
    {
        public String fixedRateTypeId { get; set; }
        public string fixedRateTypeName { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}