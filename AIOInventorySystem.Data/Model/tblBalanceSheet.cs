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
    
    public partial class tblBalanceSheet
    {
        public int Id { get; set; }
        public string GroupNameDr { get; set; }
        public string NameDr { get; set; }
        public Nullable<decimal> NAmountDr { get; set; }
        public Nullable<decimal> GAmountDr { get; set; }
        public string GroupNameCr { get; set; }
        public string NameCr { get; set; }
        public Nullable<decimal> NAmountCr { get; set; }
        public Nullable<decimal> GAmountCr { get; set; }
    }
}
