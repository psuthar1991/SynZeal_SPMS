//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_BatchFormData_Sync.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PollAnswer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PollAnswer()
        {
            this.PollVotingRecords = new HashSet<PollVotingRecord>();
        }
    
        public int Id { get; set; }
        public int PollId { get; set; }
        public string Name { get; set; }
        public int NumberOfVotes { get; set; }
        public int DisplayOrder { get; set; }
    
        public virtual Poll Poll { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PollVotingRecord> PollVotingRecords { get; set; }
    }
}
