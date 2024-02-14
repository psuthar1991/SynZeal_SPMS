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
    
    public partial class SZ_Inventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SZ_Inventory()
        {
            this.SZ_InventoryLog = new HashSet<SZ_InventoryLog>();
            this.SZ_MasterCOA = new HashSet<SZ_MasterCOA>();
            this.SZ_ProjectDetail = new HashSet<SZ_ProjectDetail>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_InventoryLog> SZ_InventoryLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_MasterCOA> SZ_MasterCOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_ProjectDetail> SZ_ProjectDetail { get; set; }
    }
}
