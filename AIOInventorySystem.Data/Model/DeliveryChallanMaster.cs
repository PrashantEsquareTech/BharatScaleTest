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
    
    public partial class DeliveryChallanMaster
    {
        public DeliveryChallanMaster()
        {
            this.DeliveryChallanDetails = new HashSet<DeliveryChallanDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> DeliveryChallanNo { get; set; }
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> Status { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
        public string VehicalNo { get; set; }
    
        public virtual ICollection<DeliveryChallanDetail> DeliveryChallanDetails { get; set; }
    }
}