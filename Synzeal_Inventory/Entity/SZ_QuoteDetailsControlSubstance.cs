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
    
    public partial class SZ_QuoteDetailsControlSubstance
    {
        public int Id { get; set; }
        public int QuoteDetailId { get; set; }
        public string Path { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Filename { get; set; }
    
        public virtual SZ_QuotationDetail SZ_QuotationDetail { get; set; }
    }
}
