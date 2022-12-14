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
    
    public partial class ServiceInvoiceMaster
    {
        public ServiceInvoiceMaster()
        {
            this.ServiceInvoiceDetails = new HashSet<ServiceInvoiceDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ServiceNo { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CLMNo { get; set; }
        public string PavatiNo { get; set; }
        public Nullable<decimal> TotalServiceCharge { get; set; }
        public Nullable<decimal> ServiceLateFee { get; set; }
        public Nullable<decimal> ServiceNetAmt { get; set; }
        public Nullable<decimal> TotalVerficationFee { get; set; }
        public Nullable<decimal> VerficationLateFee { get; set; }
        public Nullable<decimal> VerificationNetAmt { get; set; }
        public Nullable<int> CompId { get; set; }
        public string TransactionYear { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> RenewalDate { get; set; }
        public string Vaidhmapan { get; set; }
        public Nullable<int> GSTPercent { get; set; }
        public Nullable<decimal> ServiceGSTNetAmount { get; set; }
    
        public virtual ICollection<ServiceInvoiceDetail> ServiceInvoiceDetails { get; set; }
    }
}
