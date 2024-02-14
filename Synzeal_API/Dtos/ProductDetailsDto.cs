using Synzeal_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Dtos
{
    public class ProductDetailsDto
    {
        public ProductDetailsDto()
        {
            DefaultPictureModel = new PictureModelDto();
            PictureModels = new List<PictureModelDto>();
            InventoryModel = new List<ProductSZInventoryModelDto>();
        }
        public Product ProductData { get; set; }
        //picture(s)
        public int Id { get; set; }
        public bool DefaultPictureZoomEnabled { get; set; }
        public PictureModelDto DefaultPictureModel { get; set; }
        public IList<PictureModelDto> PictureModels { get; set; }
        public string FirstCatName { get; set; }
        public string CasNo { get; set; }
        public string MasterCOAOfProduct { get; set; }
        public string MainCatName { get; set; }
        public string MainCatSename { get; set; }
        public string ProductNature { get; set; }
        public Nullable<int> MainCatId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ProductTemplateViewPath { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public string DrugApiCode { get; set; }
        public string MolecularWeight { get; set; }
        public string ChemicalName { get; set; }
        public string Synonym { get; set; }
        public string Smile { get; set; }
        public string InchiKey { get; set; }
        public string Inchi { get; set; }
        public bool ShowSku { get; set; }
        public string Sku { get; set; }
        public bool ShowManufacturerPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public bool ShowGtin { get; set; }
        public string Gtin { get; set; }
        public bool ShowVendor { get; set; }
        
        public bool HasSampleDownload { get; set; }

        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool FreeShippingNotificationEnabled { get; set; }
        public string DeliveryDate { get; set; }
        public bool IsRental { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public string StockAvailability { get; set; }
        public bool DisplayBackInStockSubscription { get; set; }
        public bool EmailAFriendEnabled { get; set; }
        public bool CompareProductsEnabled { get; set; }
        public string PageShareCode { get; set; }
        public string ProductInstockStatus { get; set; }
        public bool IsDisplayBatchData { get; set; }
        public bool IsDisplayPriceData { get; set; }
        public string DataReference { get; set; }
        public List<ProductSZInventoryModelDto> InventoryModel { get; set; }
        public string INRPrice { get; set; }
        public string USDPrice { get; set; }
        public string RelatedCasNo { get; set; }

        public string RelCATNo { get; set; }

        public string QuotePrice { get; set; }
        public IList<Product> MatchingProduct { get; set; }

        public bool? IsControlledSubstance { get; set; }
        public bool? IsStructureVerified { get; set; }
        public string ParentcatName { get; set; }
    }
}
