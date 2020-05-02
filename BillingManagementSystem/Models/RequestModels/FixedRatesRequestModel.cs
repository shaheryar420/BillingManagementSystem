using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class FixedRatesRequestModel
    {
        public string userId { get; set; }
        public string fixedRatesId { get; set; }
        public string fixedRatesName { get; set; }
        public string fixedRatesAmount { get; set; }
        public string fk_type { get; set; }
        public string fixedRatesUnit { get; set; }
    }
}