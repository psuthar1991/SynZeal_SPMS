using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public partial class ProductDetailsModel
    {
        public ProductDetailsModel()
        {
            DefaultPictureModel = new PictureModel();
            PictureModels = new List<PictureModel>();
            GiftCard = new GiftCardModel();
            ProductPrice = new ProductPriceModel();
            AddToCart = new AddToCartModel();
            ProductAttributes = new List<ProductAttributeModel>();
            AssociatedProducts = new List<ProductDetailsModel>();
            InventoryModel = new List<ProductSZInventoryModel>();
            Breadcrumb = new ProductBreadcrumbModel();
            TierPrices = new List<TierPriceModel>();
        }

        //picture(s)
        public bool? IsManuallyControlledSubstance { get; set; }

        public bool? IsControlledSubstance { get; set; }
        public int Id { get; set; }
        public bool DefaultPictureZoomEnabled { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
        public IList<PictureModel> PictureModels { get; set; }
        public string FirstCatName { get; set; }
        public string MainCatName { get; set; }

        public string ChemicalName { get; set; }
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
        public string Synonym { get; set; }
        public string Smile { get; set; }
        public string ShippingCondition { get; set; }
        public bool ShowSku { get; set; }
        public string Sku { get; set; }

        public bool ShowManufacturerPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }

        public bool ShowGtin { get; set; }
        public string Gtin { get; set; }

        public bool ShowVendor { get; set; }

        public bool HasSampleDownload { get; set; }

        public GiftCardModel GiftCard { get; set; }

        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool FreeShippingNotificationEnabled { get; set; }
        public string DeliveryDate { get; set; }


        public bool IsRental { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public string ProductInstockStatus { get; set; }
        public string StockAvailability { get; set; }

        public bool DisplayBackInStockSubscription { get; set; }

        public bool EmailAFriendEnabled { get; set; }
        public bool CompareProductsEnabled { get; set; }
        public string QuotePrice { get; set; }
        public string PageShareCode { get; set; }

        public ProductPriceModel ProductPrice { get; set; }

        public AddToCartModel AddToCart { get; set; }

        public ProductBreadcrumbModel Breadcrumb { get; set; }


        public IList<ProductAttributeModel> ProductAttributes { get; set; }

        public IList<TierPriceModel> TierPrices { get; set; }

        //a list of associated products. For example, "Grouped" products could have several child "simple" products
        public IList<ProductDetailsModel> AssociatedProducts { get; set; }
        public List<ProductSZInventoryModel> InventoryModel { get; set; }

        #region Nested Classes

        public partial class ProductBreadcrumbModel
        {
            public ProductBreadcrumbModel()
            {

            }

            public bool Enabled { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }

        }
        public partial class ProductSZInventoryModel
        {
            public int SZInventoryId { get; set; }
            public decimal Qty { get; set; }
            public string BatchNo { get; set; }

            public decimal? AvailableQty { get; set; }
        }
        public partial class AddToCartModel
        {
            public AddToCartModel()
            {
                this.AllowedQuantities = new List<SelectListItem>();
            }
            public int ProductId { get; set; }


            public int EnteredQuantity { get; set; }


            public bool CustomerEntersPrice { get; set; }

            public decimal CustomerEnteredPrice { get; set; }
            public String CustomerEnteredPriceRange { get; set; }

            public bool DisableBuyButton { get; set; }
            public bool DisableWishlistButton { get; set; }
            public List<SelectListItem> AllowedQuantities { get; set; }

            //rental
            public bool IsRental { get; set; }

            //pre-order
            public bool AvailableForPreOrder { get; set; }
            public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

            //updating existing shopping cart item?
            public int UpdatedShoppingCartItemId { get; set; }
        }

        public partial class ProductPriceModel
        {
            /// <summary>
            /// The currency (in 3-letter ISO 4217 format) of the offer price 
            /// </summary>
            public string CurrencyCode { get; set; }

            public string OldPrice { get; set; }

            public string Price { get; set; }
            public string PriceWithDiscount { get; set; }

            public decimal PriceValue { get; set; }
            public decimal PriceWithDiscountValue { get; set; }

            public bool CustomerEntersPrice { get; set; }

            public bool CallForPrice { get; set; }

            public int ProductId { get; set; }

            public bool HidePrices { get; set; }

            //rental
            public bool IsRental { get; set; }
            public string RentalPrice { get; set; }

            /// <summary>
            /// A value indicating whether we should display tax/shipping info (used in Germany)
            /// </summary>
            public bool DisplayTaxShippingInfo { get; set; }
            /// <summary>
            /// PAngV baseprice (used in Germany)
            /// </summary>
            public string BasePricePAngV { get; set; }
        }

        public partial class GiftCardModel
        {
            public bool IsGiftCard { get; set; }

            public string RecipientName { get; set; }

            public string RecipientEmail { get; set; }

            public string SenderName { get; set; }

            public string SenderEmail { get; set; }

            public string Message { get; set; }

        }

        public partial class TierPriceModel
        {
            public string Price { get; set; }

            public int Quantity { get; set; }
        }

        public partial class ProductAttributeModel
        {
            public ProductAttributeModel()
            {
                AllowedFileExtensions = new List<string>();
                Values = new List<ProductAttributeValueModel>();
            }

            public int ProductId { get; set; }

            public int ProductAttributeId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string TextPrompt { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Default value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }
            /// <summary>
            /// Selected day value for datepicker
            /// </summary>
            public int? SelectedDay { get; set; }
            /// <summary>
            /// Selected month value for datepicker
            /// </summary>
            public int? SelectedMonth { get; set; }
            /// <summary>
            /// Selected year value for datepicker
            /// </summary>
            public int? SelectedYear { get; set; }

            /// <summary>
            /// Allowed file extensions for customer uploaded files
            /// </summary>
            public IList<string> AllowedFileExtensions { get; set; }

            public IList<ProductAttributeValueModel> Values { get; set; }

        }
        public partial class PictureModel
        {
            public string ImageUrl { get; set; }

            public string FullSizeImageUrl { get; set; }

            public string Title { get; set; }

            public string AlternateText { get; set; }
        }
        public partial class ProductAttributeValueModel
        {
            public ProductAttributeValueModel()
            {
                PictureModel = new PictureModel();
            }

            public string Name { get; set; }

            public string ColorSquaresRgb { get; set; }

            public string PriceAdjustment { get; set; }

            public decimal PriceAdjustmentValue { get; set; }

            public bool IsPreSelected { get; set; }

            //picture model is used when we want to override a default product picture when some attribute is selected
            public PictureModel PictureModel { get; set; }
        }

        public class SAPRawMaterialModel
        {
            public int? SrNo { get; set; }
            public string Requester { get; set; }
            public string Req_UserName { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string CASNo { get; set; }
            public string CatlogNum { get; set; }
            public string PackSize { get; set; }
            public string Make { get; set; }
            public DateTime? PR_Date { get; set; }
            public int? PR_Number { get; set; }
            public DateTime? PO_Date { get; set; }
            public int? PO_Number { get; set; }
            public decimal? PO_Qty { get; set; }
            public decimal? PO_Remain_Qty { get; set; }
            public string PO_Delivery_Date { get; set; }
            public int? Days_Req_2_TillDate { get; set; }
            public string Project { get; set; }
            public DateTime? GRPO_Date { get; set; }
            public string GRPO_Number { get; set; }
            public string WhsCode { get; set; }

            public string PR_Required_Date { get; set; }
            public string PO_Free_Text { get; set; }
            public string PO_Item_Details { get; set; }
            public string PO_Line_Remark { get; set; }
            public string PR_Doc_Remark { get; set; }
            public string PO_Doc_Remark { get; set; }
            public string GRPO_Doc_Remark { get; set; }
        }

        public class SAPCASNOModel
        {
            public string CASNo { get; set; }

        }

        public class SAPProjectModel
        {
            public string Project { get; set; }
        }


        #endregion
    }
}