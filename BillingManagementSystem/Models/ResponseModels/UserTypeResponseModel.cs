using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class UserTypeResponseModel
    {
        public string userTypeId { get; set; }
        public string userTypeName { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}