using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ActionResponseModel
    {
        public string actionId { get; set; }
        public string actionName { get; set; }
        public string actionDescription { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}