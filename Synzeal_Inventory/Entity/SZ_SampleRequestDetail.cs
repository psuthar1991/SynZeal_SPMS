//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Inventory.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_SampleRequestDetail
    {
        public int Id { get; set; }
        public int SampleRequestId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string BatchCode { get; set; }
        public int BatchId { get; set; }
        public int Qty { get; set; }
        public string Reason { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Handoverby { get; set; }
        public Nullable<System.DateTime> ResponseDate { get; set; }
        public string RejectReason { get; set; }
    
        public virtual SZ_SampleRequest SZ_SampleRequest { get; set; }
    }
}
