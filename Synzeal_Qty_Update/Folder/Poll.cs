//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Qty_Update.Folder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Poll
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poll()
        {
            this.PollAnswers = new HashSet<PollAnswer>();
        }
    
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string SystemKeyword { get; set; }
        public bool Published { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool AllowGuestsToVote { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<System.DateTime> StartDateUtc { get; set; }
        public Nullable<System.DateTime> EndDateUtc { get; set; }
    
        public virtual Language Language { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
    }
}
