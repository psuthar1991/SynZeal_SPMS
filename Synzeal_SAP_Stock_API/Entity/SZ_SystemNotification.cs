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
    
    public partial class SZ_SystemNotification
    {
        public int Id { get; set; }
        public int SentUserId { get; set; }
        public string SentUsername { get; set; }
        public int ReceivedUserId { get; set; }
        public string ReceivedUserName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsView { get; set; }
        public int ViewedUserId { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
    }
}
