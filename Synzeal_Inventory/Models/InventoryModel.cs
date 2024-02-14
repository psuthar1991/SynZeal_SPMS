using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synzeal_Inventory.Entity;

namespace Synzeal_Inventory.Models
{
    public class InventoryModel
    {
        public  InventoryModel()
        { CategoryPriceList = new List<SelectListItem>(); }
        public int ProductId { get; set; }
        public string DrugApiCode { get; set; }
        public Product Product { get; set; }
        public string Gtin { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string MolecularWeight { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Smile { get; set; }
        public string Synonym { get; set; }
        public string MainCatName { get; set; }
        public string BatchNo { get; set; }
        public decimal? Qty { get; set; }
        public string COAPath { get; set; }
        public string StdDataPath { get; set; }
        public string AddDataPath { get; set; }
        public string Remarks { get; set; }
        public string Appearance { get; set; }
        public int? InvId { get; set; }
        public decimal? Price { get; set; }
        public string Currancy { get; set; }
        public DateTime? ReTestDate { get; set; }
        public int? PriceId { get; set; }
        public string PriceRemark { get; set; }
        public DateTime? ActualDispatch { get; set; }
        
        public string INRPrice { get; set; }
        public string USDPrice { get; set; }
        public string LeadTime { get; set; }
        public string MaxDiscount { get; set; }
        public PictureModel DefaultPictureModel { get; set; }


        public string PONo { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string Company { get; set; }
        public string Scientist { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ScheduleDispatch { get; set; }
        public string NoOfPack { get; set; }
        public string PackSize { get; set; }
        public Nullable<decimal> ProjectQty { get; set; }
        public string ProjectLeadTime { get; set; }
        public string Revised1 { get; set; }
        public string Revised2 { get; set; }
        public string Revised3 { get; set; }
        public int ProjectId { get; set; }
        public int ProjectDetailId { get; set; }
        public string LabelRequirement { get; set; }
        public string DataRequired { get; set; }

        public int? InvoiceId { get; set; }
        public Nullable<System.DateTime> DispatchDate { get; set; }
        public string BasicValue { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentTerm { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Courier { get; set; }
        public string TrackingNo { get; set; }
        public string DispatchDetail { get; set; }
        public string DeliveryConfirmation { get; set; }
        public string PaymentFollowUp { get; set; }
        public string PaymentDetail { get; set; }
        public string CForm { get; set; }
        public string FirstEMail { get; set; }
        public string SecondEmail { get; set; }
        public string ContactNo { get; set; }
        public string PaymentStatus { get; set; }
        public string Category { get; set; }

        public int CategoryPriceId { get; set; }
        public IList<SelectListItem> CategoryPriceList { get; set; }
        


    }
    public partial class PictureModel
    {
        public string ImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }

    public partial class InventoryOutput
    {
        public int total { get; set; }

        public int page { get; set; }

        public int records { get; set; }

        public List<InventoryModel> rows { get; set; }
    }

    public partial class ExcelInventoryModel
    {
        public int total { get; set; }

        public int page { get; set; }

        public int records { get; set; }

        public List<InventoryModel> rows { get; set; }
    }
    // return value class  
    public class ReturnValue
    {
        // constructor  
        public ReturnValue()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

        // properties  
        public bool Success = false;
        public string Message = string.Empty;
        public Byte[] Data = null;
    }
}