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
    
    public partial class SZ_PurchaseRFQ
    {
        public int Id { get; set; }
        public string RfqNo { get; set; }
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public string ChemicalName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string Comment { get; set; }
        public string Summary { get; set; }
        public string PurchaseStatus { get; set; }
        public string PurchaseRemark { get; set; }
        public Nullable<System.DateTime> Estdate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string RefBy { get; set; }
    }
}