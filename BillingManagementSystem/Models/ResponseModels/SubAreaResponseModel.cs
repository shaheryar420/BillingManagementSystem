﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class SubAreaResponseModel
    {
        public string subAreaId { get; set; }
        public string subAreaName { get; set; }
        public string fk_area { get; set; }
        public string areaName { get; set; }
        public string noOfConsumers { get; set; }
        public string totalElectricUnits { get; set; }
        public string totalElectricAmount { get; set; }
        public string totalGasAmount { get; set; }
        public string totalGasUnits { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}