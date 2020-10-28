using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillGasCalculationResponseModel
    {
        public string totalAmount { get; set; }
        public string GST { get; set; }
        public string meterRent { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}