//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_SAP_Stock_API.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_UserManagement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyIds { get; set; }
        public bool IsNotification { get; set; }
        public bool IsPaymentShow { get; set; }
        public bool isNewsLetter { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}