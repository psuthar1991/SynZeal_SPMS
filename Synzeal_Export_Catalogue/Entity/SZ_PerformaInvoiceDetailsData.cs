//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Export_Catalogue.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_PerformaInvoiceDetailsData
    {
        public int Id { get; set; }
        public Nullable<int> PerformaInvoiceId { get; set; }
        public Nullable<int> QuoteDetailsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string QuoteDetailsPrice { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal Coldshipment { get; set; }
        public decimal TraceabilityCost { get; set; }
        public decimal AdditionTest { get; set; }
        public bool IsClub { get; set; }
    }
}
