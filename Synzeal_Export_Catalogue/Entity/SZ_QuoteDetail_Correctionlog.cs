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
    
    public partial class SZ_QuoteDetail_Correctionlog
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int QuoteDetailsId { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsClosed { get; set; }
        public string QuoteBy { get; set; }
    
        public virtual SZ_QuotationDetail SZ_QuotationDetail { get; set; }
    }
}