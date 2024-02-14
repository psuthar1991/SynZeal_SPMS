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
    
    public partial class SZ_InvoiceData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SZ_InvoiceData()
        {
            this.SZ_InvoiceDetailsData = new HashSet<SZ_InvoiceDetailsData>();
        }
    
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public System.DateTime PODate { get; set; }
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string TrackingNo { get; set; }
        public string Courier { get; set; }
        public string TermsId { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string ShipAdd1 { get; set; }
        public string ShipAdd2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public string Telno { get; set; }
        public string ShipTelno { get; set; }
        public string ShipPostCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
        public Nullable<int> ShippingCost { get; set; }
        public string Temperature { get; set; }
        public string GrossWeight { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_InvoiceDetailsData> SZ_InvoiceDetailsData { get; set; }
    }
}
