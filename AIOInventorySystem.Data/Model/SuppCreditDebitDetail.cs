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
    
    public partial class SuppCreditDebitDetail
    {
        public int Id { get; set; }
        public Nullable<int> SuppCreditDebitNo { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Reason { get; set; }
        public Nullable<int> POID { get; set; }
        public string PurchaseUnit { get; set; }
        public Nullable<decimal> SGstPercent { get; set; }
        public Nullable<decimal> SGstAmt { get; set; }
        public Nullable<decimal> CGstPercent { get; set; }
        public Nullable<decimal> CGstAmt { get; set; }
        public Nullable<decimal> IGstPercent { get; set; }
        public Nullable<decimal> IGstAmt { get; set; }
        public Nullable<decimal> Cess { get; set; }
        public Nullable<decimal> CessAmt { get; set; }
        public string AssemblyChildStatus { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> SpcDiscount { get; set; }
        public Nullable<decimal> ThreeLevelDicount { get; set; }
    
        public virtual SuppCreditDebitMaster SuppCreditDebitMaster { get; set; }
    }
}
