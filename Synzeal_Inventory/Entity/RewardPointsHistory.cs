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
    
    public partial class RewardPointsHistory
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public int PointsBalance { get; set; }
        public decimal UsedAmount { get; set; }
        public string Message { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public Nullable<int> UsedWithOrder_Id { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
