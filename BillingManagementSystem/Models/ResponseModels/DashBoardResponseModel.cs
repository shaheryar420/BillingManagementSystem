using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class DashBoardResponseModel
    {
        public string totalResidents { get; set; }
        public string totalOutstandings { get; set; }
        public string monthlyOutstandings { get; set; }
        public string monthlyRecovery { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}