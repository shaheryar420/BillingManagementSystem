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
    
    public partial class tbl_slabs
    {
        public int slab_id { get; set; }
        public string slab_tariff { get; set; }
        public Nullable<double> slab_energy_charges { get; set; }
        public Nullable<double> slab_variables_charges { get; set; }
        public Nullable<double> slab_further_charges { get; set; }
        public Nullable<double> slab_excise_duty { get; set; }
        public Nullable<double> slab_gst { get; set; }
        public Nullable<double> slab_net_rate { get; set; }
        public Nullable<double> slab_amount { get; set; }
        public Nullable<double> slab_total_amount { get; set; }
        public string slab_remarks { get; set; }
        public int fk_tarrifcategory { get; set; }
    }
}
