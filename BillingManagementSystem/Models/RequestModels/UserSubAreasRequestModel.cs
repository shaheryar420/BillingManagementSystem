using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class UserSubAreasRequestModel
    {
        public string userAreasId { get; set; }
        public string fk_user { get; set; }
        public string fk_subarea { get; set; }
    }
}