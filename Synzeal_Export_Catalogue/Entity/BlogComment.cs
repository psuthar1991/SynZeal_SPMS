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
    
    public partial class BlogComment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CommentText { get; set; }
        public int BlogPostId { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    
        public virtual BlogPost BlogPost { get; set; }
        public virtual Customer Customer { get; set; }
    }
}