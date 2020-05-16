using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class SubAreaRequestModel
    {
        public string subAreaId { get; set; }
        public string subAreaName { get; set; }
        public string fk_area { get; set; }
        public string userId { get; set; }
    }
}