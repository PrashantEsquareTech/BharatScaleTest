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
    
    public partial class QuotationDetail
    {
        public int Id { get; set; }
        public int QuotationNo { get; set; }
        public string ProductId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public string Code { get; set; }
        public string SaleUnit { get; set; }
        public Nullable<decimal> SGstPercent { get; set; }
        public Nullable<decimal> SGstAmt { get; set; }
        public Nullable<decimal> CGstPercent { get; set; }
        public Nullable<decimal> CGstAmt { get; set; }
        public Nullable<decimal> IGstPercent { get; set; }
        public Nullable<decimal> IGstAmt { get; set; }
        public string HSNCode { get; set; }
        public Nullable<decimal> SaleRate { get; set; }
        public string Prodsubtitle { get; set; }
    
        public virtual QuotationMaster QuotationMaster { get; set; }
    }
}
