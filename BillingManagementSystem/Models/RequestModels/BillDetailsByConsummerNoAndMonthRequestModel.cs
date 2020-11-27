using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillDetailsByConsummerNoAndMonthRequestModel
    {
        public string locationElectricMeterNo { get; set; }
        public string billingMonth { get; set; }
        public string residentId { get; set; }
    }
}