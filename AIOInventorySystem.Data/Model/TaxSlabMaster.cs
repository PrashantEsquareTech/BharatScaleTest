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
    
    public partial class TaxSlabMaster
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> SlabFrom { get; set; }
        public Nullable<int> SlabTo { get; set; }
        public Nullable<int> GstPercent { get; set; }
        public Nullable<int> CompId { get; set; }
    }
}