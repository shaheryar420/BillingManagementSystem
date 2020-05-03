using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ResidentResponseModel
    {
        public string outstanding { get; set; }
        public string meterNo { get; set; }
        public string previousReading { get; set; }
        public string residentId { get; set; }
        public string residentPaNumber { get; set; }
        public string residentRank { get; set; }
        public string residentName { get; set; }
        public string residentUnit { get; set; }
        public string residentRemarks { get; set; }
        public string totatOutStandings { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}