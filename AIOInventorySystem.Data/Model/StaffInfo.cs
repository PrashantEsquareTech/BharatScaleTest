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
    
    public partial class StaffInfo
    {
        public StaffInfo()
        {
            this.tblProjectAtGlanceMasters = new HashSet<tblProjectAtGlanceMaster>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string AdharCardNo { get; set; }
        public Nullable<int> CompId { get; set; }
    
        public virtual ICollection<tblProjectAtGlanceMaster> tblProjectAtGlanceMasters { get; set; }
    }
}
