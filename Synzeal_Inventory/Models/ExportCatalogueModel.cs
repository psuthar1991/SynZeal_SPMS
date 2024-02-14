using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ExportCatalogueModel
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Synonym { get; set; }
        public string MolF { get; set; }
        public string MolW { get; set; }
        public string InventoryStatus { get; set; }
        public string ImagePath { get; set; }

        public string CATNo { get; set; }
        public string CASNo { get; set; }
    }

    public class ExportCatalogueExcelModel
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Synonym { get; set; }
        public string MolF { get; set; }
        public string MolW { get; set; }
        public string InventoryStatus { get; set; }
        public string CATNo { get; set; }
        public string CASNo { get; set; }
        public string ImagePath { get; set; }
    }

    public class ExportAllProductModel
    {
        public int SrNo { get; set; }
        public string ProductName { get; set; }
        public string CATNo { get; set; }
        public string CASNo { get; set; }
        public string InventoryStatus { get; set; }
    }

    public class ExportScientistModel
    {
        public Nullable<int> ProductId { get; set; }
        public int? BatchId { get; set; }
        public string BatchCode { get; set; }
        public string SubscientistName { get; set; }
        public string ScientistName { get; set; }
        public string ProductName { get; set; }
        public string CATNo { get; set; }
        public string CASNo { get; set; }
        public string Purity { get; set; }
        public string Quantity { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string SubmissionDateText { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public string PurificationBy { get; set; }
        public int? NoOfFinalStep { get; set; }
        public string LastStatus { get; set; }
        public string Reason { get; set; }
        public string QueryReceived { get; set; }
        public string Justification { get; set; }
        public string Price { get; set; }
        public string LeadTime { get; set; }
        public string ProductRemark { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}