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
    
    public partial class PurchaseReturnMaster
    {
        public PurchaseReturnMaster()
        {
            this.PurchaseReturnDetails = new HashSet<PurchaseReturnDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> PurchaseReturnNo { get; set; }
        public Nullable<System.DateTime> PurchaseReturnDate { get; set; }
        public Nullable<int> PorderNo { get; set; }
        public Nullable<System.DateTime> PorderDate { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Suppliername { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> SpcDiscount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public string SupplierpoNo { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
    
        public virtual ICollection<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
    }
}