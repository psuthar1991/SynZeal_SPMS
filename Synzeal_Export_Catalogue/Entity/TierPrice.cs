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
    
    public partial class TierPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public Nullable<int> CustomerRoleId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    
        public virtual CustomerRole CustomerRole { get; set; }
        public virtual Product Product { get; set; }
    }
}
