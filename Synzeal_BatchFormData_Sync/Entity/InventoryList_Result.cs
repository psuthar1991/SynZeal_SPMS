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
    
    public partial class InventoryList_Result
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string BatchNo { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Appearance { get; set; }
        public string COAPath { get; set; }
        public string StdDataPath { get; set; }
        public string AddDataPath { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ReTestDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<decimal> DeepQty { get; set; }
        public Nullable<decimal> RegularQty { get; set; }
        public string ReTestRemark { get; set; }
        public Nullable<decimal> ReservedQty { get; set; }
        public Nullable<decimal> AvailableQty { get; set; }
    }
}
