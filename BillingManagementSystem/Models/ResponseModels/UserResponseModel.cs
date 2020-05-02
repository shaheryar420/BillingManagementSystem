using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class UserResponseModel
    {
        public string usersId { get; set; }
        public string usersUsername { get; set; }
        public string usersFullName { get; set; }
        public string usersPassword { get; set; }
        public string fk_userType { get; set; }
        public string userTypeName { get; set; }
        public string usersIsActive { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}