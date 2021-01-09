using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models.RequestModels
{
    public class ConsummerPoolRequestModel
    {
        public string consummerNoId { get; set; }
        public string consummerNo { get; set; }
        public string fk_location { get; set; }
        public string is_active { get; set; }
    }
}