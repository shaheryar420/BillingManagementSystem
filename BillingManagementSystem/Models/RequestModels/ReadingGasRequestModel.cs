using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ReadingGasRequestModel
    {
        public string userId { get; set; }
        public string amount { get; set; }
        public string readingpicture_data { get; set; }
        public string readingpicture_type { get; set; }
        public string readingpicture_size { get; set; }
        public string readingGasId { get; set; }
        public string readingGasDatetime { get; set; }
        public string readingGasRemarks { get; set; }
        public string readingGasMeterNo { get; set; }
        public string readingGasPrevReading { get; set; }
        public string readingGasCurrentReading { get; set; }
        public string fk_readingPicture { get; set; }
        public string readingGasUnits { get; set; }
        public string readingGasAddedby { get; set; }
        public string readingGasMonth { get; set; }
    }
}