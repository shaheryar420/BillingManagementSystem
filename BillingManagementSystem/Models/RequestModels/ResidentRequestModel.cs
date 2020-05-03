using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ResidentRequestModel
    {
        public string residentId { get; set; }
        public string residentPaNumber { get; set; }
        public string residentRank { get; set; }
        public string residentName { get; set; }
        public string residentUnit { get; set; }
        public string residentRemarks { get; set; }
    }
}