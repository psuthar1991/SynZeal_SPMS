//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_AutoBackup.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_Quotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SZ_Quotation()
        {
            this.SZ_QuotationDetail = new HashSet<SZ_QuotationDetail>();
        }
    
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsImageAttach { get; set; }
        public string PONo { get; set; }
        public string Remark { get; set; }
        public Nullable<int> TermsId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Ref { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<bool> IsImportedQuote { get; set; }
        public string CountryType { get; set; }
        public Nullable<bool> IsToBe { get; set; }
        public string ClientRef { get; set; }
        public string Attachment { get; set; }
        public string SuggChemName { get; set; }
        public Nullable<bool> IsPayment { get; set; }
        public Nullable<bool> Auction { get; set; }
        public string EmailCC { get; set; }
        public Nullable<bool> IsQuoteApproved { get; set; }
        public string UserDistType { get; set; }
        public Nullable<bool> IsRemoveFollowup { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual SZ_CompanyList SZ_CompanyList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_QuotationDetail> SZ_QuotationDetail { get; set; }
    }
}
