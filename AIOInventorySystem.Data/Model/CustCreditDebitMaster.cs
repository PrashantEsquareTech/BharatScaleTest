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
    
    public partial class CustCreditDebitMaster
    {
        public CustCreditDebitMaster()
        {
            this.CustCreditDebitDetails = new HashSet<CustCreditDebitDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CustCreditDebitNo { get; set; }
        public Nullable<System.DateTime> CustCreditDebitDate { get; set; }
        public string BillNo { get; set; }
        public Nullable<System.DateTime> Billdate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Free { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CDType { get; set; }
    
        public virtual ICollection<CustCreditDebitDetail> CustCreditDebitDetails { get; set; }
    }
}