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
    
    public partial class tbl_paymentgashistory
    {
        public int paymenthistory_id { get; set; }
        public System.DateTime paymenthistory_datetime { get; set; }
        public double payment_amount { get; set; }
        public int fk_paymenttype { get; set; }
        public string paymentmonth { get; set; }
        public int fk_resident { get; set; }
    }
}
