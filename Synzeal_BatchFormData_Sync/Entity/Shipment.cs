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
    
    public partial class Shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment()
        {
            this.ShipmentItems = new HashSet<ShipmentItem>();
        }
    
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }
        public Nullable<System.DateTime> ShippedDateUtc { get; set; }
        public Nullable<System.DateTime> DeliveryDateUtc { get; set; }
        public string AdminComment { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    
        public virtual Order Order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
