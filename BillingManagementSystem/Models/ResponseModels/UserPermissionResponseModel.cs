using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class UserPermissionResponseModel
    {
        public string userpermissions_id { get; set; }
        public string fk_action { get; set; }
        public string fk_user { get; set; }
        public string userpermissions_action { get; set; }
        public string userpermissions_controller { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}