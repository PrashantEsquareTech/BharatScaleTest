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
    
    public partial class StockOpening
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string MfgCompany { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> OpeningQty { get; set; }
        public Nullable<bool> Vat { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Size { get; set; }
        public string ColorName { get; set; }
    }
}
