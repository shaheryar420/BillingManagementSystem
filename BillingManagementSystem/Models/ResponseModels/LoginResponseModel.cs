using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class LoginResponseModel
    {
        public string userTypeId { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string userFullName { get; set; }
        public string userType { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}