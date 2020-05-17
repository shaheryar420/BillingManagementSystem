using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class UserSubAreasResponseModel
    {
        public string userAreasId { get; set; }
        public string fk_user { get; set; }
        public string userName { get; set; }
        public string userUserName { get; set; }
        public string subAreaName { get; set; }
        public string fk_subarea { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}