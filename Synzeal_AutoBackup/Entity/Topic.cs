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
    
    public partial class Topic
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public bool IncludeInSitemap { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool IncludeInFooterColumn1 { get; set; }
        public bool IncludeInFooterColumn2 { get; set; }
        public bool IncludeInFooterColumn3 { get; set; }
        public int DisplayOrder { get; set; }
        public bool AccessibleWhenStoreClosed { get; set; }
        public bool IsPasswordProtected { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int TopicTemplateId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool LimitedToStores { get; set; }
    }
}
