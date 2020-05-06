using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ResidentBuildingRequestModel
    {
        public string residentBuildingId { get; set; }
        public string fk_resident { get; set; }
        public string fk_building { get; set; }
    }
}