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
    
    public partial class SZ_InvoiceDetailsData
    {
        public int Id { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Nullable<int> QuoteDetailsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<System.DateTime> AnalysisDate { get; set; }
        public Nullable<System.DateTime> RetestDate { get; set; }
        public string HSN { get; set; }
        public string Qty { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string ADCQty { get; set; }
        public string DSLNo { get; set; }
        public string FOB { get; set; }
        public string Remark { get; set; }
    
        public virtual SZ_InvoiceData SZ_InvoiceData { get; set; }
    }
}
