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
    
    public partial class NewsComment
    {
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public int NewsItemId { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual News News { get; set; }
    }
}
