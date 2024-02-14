using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class MasterCOAModel
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int COAId { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public string QuantityAvailable { get; set; }
        public Nullable<System.DateTime> AnalysisDate { get; set; }
        public string HPLCGCELSD { get; set; }
        public string Purity { get; set; }
        public string AdditionalInfor { get; set; }
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
        public string CNMRText { get; set; }
        public string StorageCon { get; set; }
        public Nullable<System.DateTime> ReTestDate { get; set; }
        public string SpecialInstruction { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string CASNO { get; set; }
        public string CATNO { get; set; }
        public string AppearanceProduct { get; set; }

        public string Attachment { get; set; }
        public int ProductId { get; set; }
        public string Chemicalname { get; set; }
        public string RefNo { get; set; }
        public bool IsEquation { get; set; }
        public string EquationType { get; set; }

        public bool? IsLogoAttached { get; set; }
        public Nullable<bool> IsManufacture { get; set; }
        public Nullable<System.DateTime> ManufactureDate { get; set; }

        public string OtherValue { get; set; }
        public bool? IsShipping { get; set; }
        public string CTheroretical { get; set; }
        public string HTheroretical { get; set; }
        public string NTheroretical { get; set; }
        public string STheroretical { get; set; }
        public string CPractical { get; set; }
        public string HPractical { get; set; }
        public string NPractical { get; set; }
        public string SPractical { get; set; }
        public string CreatedBY { get; set; }
        public string UpdatedBY { get; set; }
        public string WegithLossBy { get; set; }
        public string MeltingPoint { get; set; }

        public string qNMR { get; set; }
    }
}