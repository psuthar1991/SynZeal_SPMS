//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Qty_Update.Folder
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_QuoteDetailPrice
    {
        public int Id { get; set; }
        public int MG { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Vial { get; set; }
        public string Currency { get; set; }
        public int QuoteDetailsId { get; set; }
    
        public virtual SZ_QuotationDetail SZ_QuotationDetail { get; set; }
    }
}
