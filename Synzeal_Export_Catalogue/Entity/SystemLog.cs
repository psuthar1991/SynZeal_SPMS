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
    
    public partial class SystemLog
    {
        public int Id { get; set; }
        public string Entityname { get; set; }
        public Nullable<int> EntityId { get; set; }
        public string PropertyName { get; set; }
        public string FieldName { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Username { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
