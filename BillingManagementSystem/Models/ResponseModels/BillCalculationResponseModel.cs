using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillCalculationResponseModel
    {
        public double totalAmount { get; set; }
        public double totalFPA { get; set; }
        public double totalEnergyCharges { get; set; }
        public double meterRent { get; set; }
        public double waterCharges { get; set; }
        public double tvCharges { get; set; }
        public double furCharges { get; set; }
        public string resultCode { get; set; }
        public string remarks { get; set; }
    }
}