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
    
    public partial class BankInformation
    {
        public int ID { get; set; }
        public Nullable<int> BankId { get; set; }
        public string Bankname { get; set; }
        public string BankAddress { get; set; }
        public string BankCno { get; set; }
        public string IFSCCode { get; set; }
        public Nullable<int> ACGId { get; set; }
        public Nullable<int> CompId { get; set; }
    
        public virtual AccountingGroupMaster AccountingGroupMaster { get; set; }
    }
}
