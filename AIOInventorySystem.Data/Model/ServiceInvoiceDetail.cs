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
    
    public partial class ServiceInvoiceDetail
    {
        public int Id { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Capacity { get; set; }
        public string Min { get; set; }
        public string e { get; set; }
        public string Class { get; set; }
        public string MachineNo { get; set; }
        public Nullable<decimal> VerficationFee { get; set; }
        public Nullable<decimal> ServiceCharge { get; set; }
        public string CertificateNo { get; set; }
    
        public virtual ServiceInvoiceMaster ServiceInvoiceMaster { get; set; }
    }
}