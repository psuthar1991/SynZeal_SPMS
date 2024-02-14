using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Entities
{
    public class SZ_MasterCOA
    {
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
    }
}
