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
    
    public partial class tbEnvirnmentAndCurtainCoolpadSystemDetail
    {
        public int Id { get; set; }
        public Nullable<int> MasterId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Quantity { get; set; }
    
        public virtual tblEnvirnmentAndCurtainCoolpadSystemMaster tblEnvirnmentAndCurtainCoolpadSystemMaster { get; set; }
    }
}
