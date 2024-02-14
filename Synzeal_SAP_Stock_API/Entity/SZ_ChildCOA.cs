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
    
    public partial class SZ_ChildCOA
    {
        public int Id { get; set; }
        public Nullable<int> MasterCOAID { get; set; }
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
        public bool IsRepresentative { get; set; }
        public string EP { get; set; }
        public Nullable<bool> EPChk { get; set; }
        public string USP { get; set; }
        public Nullable<bool> USPChk { get; set; }
        public string IUPAC { get; set; }
        public Nullable<bool> IUPACChk { get; set; }
        public string OtherSynonum { get; set; }
        public string AdditionalInfor { get; set; }
        public string RefNo { get; set; }
        public string EPChemicalName { get; set; }
        public string USPChemicalName { get; set; }
        public string OtherNameOne { get; set; }
        public string OtherNameSecond { get; set; }
        public string OtherNameThird { get; set; }
        public string OtherNameFour { get; set; }
        public string OtherNameFive { get; set; }
        public string OtherNameSix { get; set; }
        public string OtherNameSeven { get; set; }
        public Nullable<bool> ChkEPChemicalName { get; set; }
        public Nullable<bool> ChkUSPChemicalName { get; set; }
        public Nullable<bool> ChkOtherNameOne { get; set; }
        public Nullable<bool> ChkOtherNameSecond { get; set; }
        public Nullable<bool> ChkOtherNameThird { get; set; }
        public Nullable<bool> ChkOtherNameFour { get; set; }
        public Nullable<bool> ChkOtherNameFive { get; set; }
        public Nullable<bool> ChkOtherNameSix { get; set; }
        public Nullable<bool> ChkOtherNameSeven { get; set; }
        public string ChemEP { get; set; }
        public Nullable<bool> ChkChemEP { get; set; }
        public string ChemUSP { get; set; }
        public Nullable<bool> ChkChemUSP { get; set; }
        public string ChemIUPAC { get; set; }
        public Nullable<bool> ChkChemIUPAC { get; set; }
        public string ChemEPChemicalName { get; set; }
        public string ChemUSPChemicalName { get; set; }
        public string ChemOtherNameOne { get; set; }
        public string ChemOtherNameSecond { get; set; }
        public string ChemOtherNameThird { get; set; }
        public string ChemOtherNameFour { get; set; }
        public string ChemOtherNameFive { get; set; }
        public string ChemOtherNameSix { get; set; }
        public string ChemOtherNameSeven { get; set; }
        public Nullable<bool> ChemChkEPChemicalName { get; set; }
        public Nullable<bool> ChemChkUSPChemicalName { get; set; }
        public Nullable<bool> ChemChkOtherNameOne { get; set; }
        public Nullable<bool> ChemChkOtherNameSecond { get; set; }
        public Nullable<bool> ChemChkOtherNameThird { get; set; }
        public Nullable<bool> ChemChkOtherNameFour { get; set; }
        public Nullable<bool> ChemChkOtherNameFive { get; set; }
        public Nullable<bool> ChemChkOtherNameSix { get; set; }
        public Nullable<bool> ChemChkOtherNameSeven { get; set; }
        public string SynEP { get; set; }
        public Nullable<bool> ChkSynEP { get; set; }
        public string SynUSP { get; set; }
        public Nullable<bool> ChkSynUSP { get; set; }
        public string SynIUPAC { get; set; }
        public Nullable<bool> ChkSynIUPAC { get; set; }
        public string SynEPChemicalName { get; set; }
        public string SynUSPChemicalName { get; set; }
        public string SynOtherNameOne { get; set; }
        public string SynOtherNameSecond { get; set; }
        public string SynOtherNameThird { get; set; }
        public string SynOtherNameFour { get; set; }
        public string SynOtherNameFive { get; set; }
        public string SynOtherNameSix { get; set; }
        public string SynOtherNameSeven { get; set; }
        public Nullable<bool> SynChkEPChemicalName { get; set; }
        public Nullable<bool> SynChkUSPChemicalName { get; set; }
        public Nullable<bool> SynChkOtherNameOne { get; set; }
        public Nullable<bool> SynChkOtherNameSecond { get; set; }
        public Nullable<bool> SynChkOtherNameThird { get; set; }
        public Nullable<bool> SynChkOtherNameFour { get; set; }
        public Nullable<bool> SynChkOtherNameFive { get; set; }
        public Nullable<bool> SynChkOtherNameSix { get; set; }
        public Nullable<bool> SynChkOtherNameSeven { get; set; }
        public string Purity { get; set; }
        public Nullable<bool> IsLogoAttached { get; set; }
        public Nullable<bool> IsEquation { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ReviewdBy { get; set; }
        public string CASNo { get; set; }
        public string MolecularWeight { get; set; }
        public string MolFormula { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> IsImageUpload { get; set; }
        public string Chemicalname { get; set; }
        public string AppearanceProduct { get; set; }
        public string CATNo { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string Attachment { get; set; }
        public string Synonym { get; set; }
        public string EquationType { get; set; }
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
    
        public virtual SZ_MasterCOA SZ_MasterCOA { get; set; }
    }
}
