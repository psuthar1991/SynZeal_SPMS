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
    
    public partial class SZ_ReactionMatrix
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public int ProductId { get; set; }
        public int QuoteDetailId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime SchemeFromDate { get; set; }
        public System.DateTime SchemeEndDate { get; set; }
        public string Scheme { get; set; }
        public int TotalStep { get; set; }
        public int StepNo { get; set; }
        public int NoofReaction { get; set; }
        public int SuccessReaction { get; set; }
        public int NoofPurification { get; set; }
        public string BatchSize { get; set; }
        public string ExpNo { get; set; }
        public int SrNo { get; set; }
    
        public virtual SZ_QuotationDetail SZ_QuotationDetail { get; set; }
    }
}