//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIOInventorySystem.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAlertForSupplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> POrderNo { get; set; }
        public Nullable<System.DateTime> POrderDate { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<System.DateTime> CreditDate { get; set; }
        public Nullable<int> Days { get; set; }
    }
}