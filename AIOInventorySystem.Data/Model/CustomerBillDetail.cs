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
    
    public partial class CustomerBillDetail
    {
        public CustomerBillDetail()
        {
            this.ManufacturingUnitInfoes = new HashSet<ManufacturingUnitInfo>();
        }
    
        public int Id { get; set; }
        public Nullable<int> BillNo { get; set; }
        public string ProductId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> SGstAmt { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string SaleUnit { get; set; }
        public Nullable<decimal> SGstPercent { get; set; }
        public Nullable<decimal> CGstAmt { get; set; }
        public Nullable<decimal> CGstPercent { get; set; }
        public Nullable<decimal> IGstAmt { get; set; }
        public Nullable<decimal> IGstPercent { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public string HSNCode { get; set; }
        public string Prodsubtitle { get; set; }
        public Nullable<decimal> SaleRate { get; set; }
        public Nullable<decimal> SpcDisc { get; set; }
        public Nullable<decimal> Cess { get; set; }
        public Nullable<decimal> CessAmt { get; set; }
        public string Barcode { get; set; }
        public string Expiry { get; set; }
        public string BatchNo { get; set; }
        public string AssemblyChildStatus { get; set; }
        public Nullable<decimal> TotalQuantitySubtitle { get; set; }
        public string ColorName { get; set; }
        public string DeptName { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> barcodePrice { get; set; }
    
        public virtual CustomerBillMaster CustomerBillMaster { get; set; }
        public virtual ICollection<ManufacturingUnitInfo> ManufacturingUnitInfoes { get; set; }
    }
}
