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
    
    public partial class Group_Newsletter_Map
    {
        public int Id { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> NewsletterId { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Newsletter Newsletter { get; set; }
    }
}