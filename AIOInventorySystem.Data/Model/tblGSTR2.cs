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
    
    public partial class tblGSTR2
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Date { get; set; }
        public string SName { get; set; }
        public string GSTIN { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }
        public Nullable<decimal> PurchaseAt5 { get; set; }
        public Nullable<decimal> CGST2x5 { get; set; }
        public Nullable<decimal> SGST2x5 { get; set; }
        public Nullable<decimal> PurchaseAt12 { get; set; }
        public Nullable<decimal> CGST6 { get; set; }
        public Nullable<decimal> SGST6 { get; set; }
        public Nullable<decimal> PurchaseAt18 { get; set; }
        public Nullable<decimal> CGST9 { get; set; }
        public Nullable<decimal> SGST9 { get; set; }
        public Nullable<decimal> PurchaseAt28 { get; set; }
        public Nullable<decimal> CGST14 { get; set; }
        public Nullable<decimal> SGST14 { get; set; }
        public Nullable<decimal> IGST5 { get; set; }
        public Nullable<decimal> IGST12 { get; set; }
        public Nullable<decimal> IGST18 { get; set; }
        public Nullable<decimal> IGST28 { get; set; }
        public Nullable<decimal> OtherCess { get; set; }
    }
}
