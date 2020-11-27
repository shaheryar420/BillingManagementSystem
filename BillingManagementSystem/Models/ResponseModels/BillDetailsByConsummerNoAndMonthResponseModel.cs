using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class BillDetailsByConsummerNoAndMonthResponseModel
    {
        public string area { get; set; }
        public string subArea { get; set; }
        public string location { get; set; }
        public string outstandings { get; set; }
        public string amount { get; set; }
        public string previousReading { get; set; }
        public string currentReading { get; set; }
        public string units { get; set; }
        public string residentId { get; set; }
        public string residentName { get; set; }
        public string mmbtu { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
        
    }
}