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
    
    public partial class AddressAttributeValue
    {
        public int Id { get; set; }
        public int AddressAttributeId { get; set; }
        public string Name { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }
    
        public virtual AddressAttribute AddressAttribute { get; set; }
    }
}
