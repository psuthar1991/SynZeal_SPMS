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
    
    public partial class Forums_Topic
    {
        public Forums_Topic()
        {
            this.Forums_Post = new HashSet<Forums_Post>();
        }
    
        public int Id { get; set; }
        public int ForumId { get; set; }
        public int CustomerId { get; set; }
        public int TopicTypeId { get; set; }
        public string Subject { get; set; }
        public int NumPosts { get; set; }
        public int Views { get; set; }
        public int LastPostId { get; set; }
        public int LastPostCustomerId { get; set; }
        public Nullable<System.DateTime> LastPostTime { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public System.DateTime UpdatedOnUtc { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Forums_Forum Forums_Forum { get; set; }
        public virtual ICollection<Forums_Post> Forums_Post { get; set; }
    }
}
