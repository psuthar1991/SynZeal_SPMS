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
    
    public partial class SZ_Retest
    {
        public int Id { get; set; }
        public string CatNo { get; set; }
        public int ProductId { get; set; }
        public System.DateTime Createddate { get; set; }
        public int InventoryId { get; set; }
        public bool IsApproved { get; set; }
        public string CreatedBy { get; set; }
        public bool IsRetestLater { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
    }
}
