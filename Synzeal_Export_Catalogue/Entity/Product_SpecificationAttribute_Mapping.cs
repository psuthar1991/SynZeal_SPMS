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
    
    public partial class Product_SpecificationAttribute_Mapping
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttributeTypeId { get; set; }
        public int SpecificationAttributeOptionId { get; set; }
        public string CustomValue { get; set; }
        public bool AllowFiltering { get; set; }
        public bool ShowOnProductPage { get; set; }
        public int DisplayOrder { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual SpecificationAttributeOption SpecificationAttributeOption { get; set; }
    }
}
