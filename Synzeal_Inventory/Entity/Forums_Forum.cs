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
    
    public partial class Forums_Forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Forums_Forum()
        {
            this.Forums_Topic = new HashSet<Forums_Topic>();
        }
    
        public int Id { get; set; }
        public int ForumGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumTopics { get; set; }
        public int NumPosts { get; set; }
        public int LastTopicId { get; set; }
        public int LastPostId { get; set; }
        public int LastPostCustomerId { get; set; }
        public Nullable<System.DateTime> LastPostTime { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public System.DateTime UpdatedOnUtc { get; set; }
    
        public virtual Forums_Group Forums_Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forums_Topic> Forums_Topic { get; set; }
    }
}
