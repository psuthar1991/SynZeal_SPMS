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
    
    public partial class CheckoutAttribute
    {
        public CheckoutAttribute()
        {
            this.CheckoutAttributeValues = new HashSet<CheckoutAttributeValue>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string TextPrompt { get; set; }
        public bool IsRequired { get; set; }
        public bool ShippableProductRequired { get; set; }
        public bool IsTaxExempt { get; set; }
        public int TaxCategoryId { get; set; }
        public int AttributeControlTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public bool LimitedToStores { get; set; }
        public Nullable<int> ValidationMinLength { get; set; }
        public Nullable<int> ValidationMaxLength { get; set; }
        public string ValidationFileAllowedExtensions { get; set; }
        public Nullable<int> ValidationFileMaximumSize { get; set; }
        public string DefaultValue { get; set; }
    
        public virtual ICollection<CheckoutAttributeValue> CheckoutAttributeValues { get; set; }
    }
}
