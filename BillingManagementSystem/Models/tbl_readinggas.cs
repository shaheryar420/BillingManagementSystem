//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillingManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_readinggas
    {
        public int reading_id { get; set; }
        public System.DateTime reading_datetime { get; set; }
        public string reading_remarks { get; set; }
        public string reading_meterno { get; set; }
        public double reading_prevreading { get; set; }
        public double reading_currentreading { get; set; }
        public int fk_readingpicture { get; set; }
        public double reading_units { get; set; }
        public int reading_addedby { get; set; }
        public string reading_month { get; set; }
        public int fk_resident { get; set; }
        public int fk_location { get; set; }
        public double MMBTU { get; set; }
    }
}
