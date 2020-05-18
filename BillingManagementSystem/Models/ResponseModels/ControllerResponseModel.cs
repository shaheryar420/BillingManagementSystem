using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ControllerResponseModel
    {
        public string controllerId { get; set; }
        public string controllerName { get; set; }
        public string controllerDescription { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}