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
    
    public partial class RepackMaster
    {
        public RepackMaster()
        {
            this.RepackDetails = new HashSet<RepackDetail>();
        }
    
        public int ID { get; set; }
        public Nullable<int> RepackNo { get; set; }
        public Nullable<System.DateTime> RepackDate { get; set; }
        public Nullable<int> CompId { get; set; }
        public string TransactionYear { get; set; }
    
        public virtual ICollection<RepackDetail> RepackDetails { get; set; }
    }
}
