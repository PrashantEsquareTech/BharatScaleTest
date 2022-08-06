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
    
    public partial class ProprietorLedger
    {
        public int Id { get; set; }
        public Nullable<int> ReceiptNo { get; set; }
        public Nullable<int> ProprietorId { get; set; }
        public string ProprietorName { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Type { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Reasons { get; set; }
    }
}
