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
    
    public partial class PurchaseOrderPlaceMaster
    {
        public PurchaseOrderPlaceMaster()
        {
            this.PurchaseOrderPlaceDetails = new HashSet<PurchaseOrderPlaceDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> PorderNo { get; set; }
        public Nullable<System.DateTime> PorderDate { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string Suppliername { get; set; }
        public string SupplierpoNo { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
    
        public virtual ICollection<PurchaseOrderPlaceDetail> PurchaseOrderPlaceDetails { get; set; }
    }
}
