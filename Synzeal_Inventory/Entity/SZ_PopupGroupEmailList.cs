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
    
    public partial class SZ_PopupGroupEmailList
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string EmailAddress { get; set; }
        public int CustomerId { get; set; }
    
        public virtual SZ_PopupGroup SZ_PopupGroup { get; set; }
    }
}
