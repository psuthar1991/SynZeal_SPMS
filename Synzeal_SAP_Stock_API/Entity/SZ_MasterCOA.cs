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
    
    public partial class SZ_MasterCOA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SZ_MasterCOA()
        {
            this.SZ_ChildCOA = new HashSet<SZ_ChildCOA>();
        }
    
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public string QuantityAvailable { get; set; }
        public Nullable<System.DateTime> AnalysisDate { get; set; }
        public string HPLCGCELSD { get; set; }
        public string TGALoss { get; set; }
        public string ResidueOnIgnition { get; set; }
        public string Potency { get; set; }
        public string PhysicalState { get; set; }
        public string SOLUBILITY { get; set; }
        public string IR { get; set; }
        public string Mass { get; set; }
        public string HPLC { get; set; }
        public string NMR { get; set; }
        public string CMR { get; set; }
        public string Dept { get; set; }
        public string TGA { get; set; }
        public string StorageCon { get; set; }
        public Nullable<System.DateTime> ReTestDate { get; set; }
        public string SpecialInstruction { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RefNo { get; set; }
        public string Purity { get; set; }
        public Nullable<bool> IsRepresentative { get; set; }
        public string Chemicalname { get; set; }
        public string AppearanceProduct { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string MolFormula { get; set; }
        public string MolecularWeight { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string Attachment { get; set; }
        public string AdditionalInfor { get; set; }
        public Nullable<bool> IsEquation { get; set; }
        public string Synonym { get; set; }
        public string EquationType { get; set; }
        public Nullable<bool> IsLogoAttached { get; set; }
        public Nullable<bool> IsManufacture { get; set; }
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        public string OtherValue { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsShipping { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CTheroretical { get; set; }
        public string HTheroretical { get; set; }
        public string NTheroretical { get; set; }
        public string STheroretical { get; set; }
        public string CPractical { get; set; }
        public string HPractical { get; set; }
        public string NPractical { get; set; }
        public string SPractical { get; set; }
        public string WegithLossBy { get; set; }
        public string MeltingPoint { get; set; }
        public string COARemark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SZ_ChildCOA> SZ_ChildCOA { get; set; }
        public virtual SZ_Inventory SZ_Inventory { get; set; }
    }
}
