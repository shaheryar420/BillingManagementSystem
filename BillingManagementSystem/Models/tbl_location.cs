//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillingManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_location
    {
        public int location_id { get; set; }
        public string location_name { get; set; }
        public int fk_subarea { get; set; }
        public string location_electricmeter { get; set; }
        public string location_gassmeter { get; set; }
        public string location_wapdameter { get; set; }
    }
}
