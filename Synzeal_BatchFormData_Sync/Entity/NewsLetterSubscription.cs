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
    
    public partial class NewsLetterSubscription
    {
        public int Id { get; set; }
        public System.Guid NewsLetterSubscriptionGuid { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public int StoreId { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    }
}
