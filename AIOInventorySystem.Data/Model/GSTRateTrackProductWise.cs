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
    
    public partial class GSTRateTrackProductWise
    {
        public int Id { get; set; }
        public Nullable<int> ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MFGCompany { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<System.DateTime> ApllicableFromDate { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<decimal> CESS { get; set; }
    }
}
