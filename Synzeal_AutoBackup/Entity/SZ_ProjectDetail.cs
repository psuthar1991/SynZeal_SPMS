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
    
    public partial class SZ_ProjectDetail
    {
        public int Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> BatchId { get; set; }
        public string NoOfPack { get; set; }
        public string PackSize { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string LeadTime { get; set; }
        public string LabelRequirement { get; set; }
        public string DataRequired { get; set; }
        public Nullable<System.DateTime> ScheduleDispatch { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string Scientist { get; set; }
        public Nullable<System.DateTime> CompletationDate { get; set; }
        public string ProjectType { get; set; }
        public string Othervalue { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual SZ_Inventory SZ_Inventory { get; set; }
        public virtual SZ_Project SZ_Project { get; set; }
    }
}