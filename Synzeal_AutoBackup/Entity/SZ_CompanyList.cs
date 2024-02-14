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
    
    public partial class SZ_CompanyList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SZ_CompanyList()
        {
            this.SZ_Quotation = new HashSet<SZ_Quotation>();
            this.SZ_EmailSuggestion = new HashSet<SZ_EmailSuggestion>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MasterEmail { get; set; }
        public string CountryType { get; set; }
        public Nullable<int> TermsId { get; set; }
        public string UserDistType { get; set; }
        public string Location { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_Quotation> SZ_Quotation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_EmailSuggestion> SZ_EmailSuggestion { get; set; }
    }
}
