//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_AutoBackup.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Newsletter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Newsletter()
        {
            this.Group_Newsletter_Map = new HashSet<Group_Newsletter_Map>();
        }
    
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public string Other { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group_Newsletter_Map> Group_Newsletter_Map { get; set; }
    }
}
