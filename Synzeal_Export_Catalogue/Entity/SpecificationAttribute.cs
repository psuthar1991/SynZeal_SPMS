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
    
    public partial class SpecificationAttribute
    {
        public SpecificationAttribute()
        {
            this.SpecificationAttributeOptions = new HashSet<SpecificationAttributeOption>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    
        public virtual ICollection<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; }
    }
}
