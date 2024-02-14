using System.Collections.Generic;
using static Synzeal_Inventory.Models.ProductDetailsModel;

namespace Synzeal_Inventory.Models
{
    public class ProductOverviewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string SeName { get; set; }
        public string DrugApiCode { get; set; }
        public string MolecularWeight { get; set; }
        public string ChemicalName { get; set; }
        public string Synonym { get; set; }
        public string Smile { get; set; }
        public string CasNo { get; set; }
        public string MolFormula { get; set; }

        public string ParentcatName { get; set; }
        public string ProductInstockStatus { get; set; }
        public bool IsDisplayBatchData { get; set; }
        public string INRPrice { get; set; }
        public string USDPrice { get; set; }
        public string LeadTime { get; set; }
        public string QuotePrice { get; set; }
        public string Quotationreceived { get; set; }
        public string PurchaseComment { get; set; }
        public bool? IsControlledSubstance { get; set; }
        public List<ProductSZInventoryModel> InventoryModel { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
        public string PurchaseSummary { get; set; }
        public Synzeal_Inventory.Controllers.ProductPriceModel PriceModel { get;set;}
    }
}