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
    
    public partial class Rfq
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string Quantity { get; set; }
        public string QuantitySym { get; set; }
        public Nullable<bool> IsSend { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Unit { get; set; }
        public string AdditionalComment { get; set; }
        public string RFQNo { get; set; }
    }
}
