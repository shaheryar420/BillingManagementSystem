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
    
    public partial class tbl_billgas
    {
        public int id { get; set; }
        public System.DateTime datetime { get; set; }
        public double amount { get; set; }
        public int fk_paymentstatus { get; set; }
        public int fk_location { get; set; }
        public int fk_resident { get; set; }
        public double prevreading { get; set; }
        public double currentreading { get; set; }
        public double units { get; set; }
        public string remarks { get; set; }
        public Nullable<int> fk_billpicture { get; set; }
        public string month { get; set; }
        public Nullable<double> outstanding { get; set; }
        public double fpa { get; set; }
        public double rebate { get; set; }
        public double paymentAmount { get; set; }
        public Nullable<System.DateTime> paymentDate { get; set; }
    }
}
