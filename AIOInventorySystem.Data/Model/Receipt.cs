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
    
    public partial class Receipt
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> receiptDate { get; set; }
        public Nullable<int> receiptno { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public Nullable<double> PaidAmount { get; set; }
        public Nullable<double> RemainingAmount { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Customername { get; set; }
        public string Paymentmode { get; set; }
        public string bankname { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
    }
}
