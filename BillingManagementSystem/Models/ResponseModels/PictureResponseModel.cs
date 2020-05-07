using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class PictureResponseModel
    {
        public string pictiureId { get; set; }
        public string pictureData { get; set; }
        public string pictureType { get; set; }
        public string pictureSize { get; set; }
        public string remarks { get; set; }
        public string resultCode { get; set; }
    }
}