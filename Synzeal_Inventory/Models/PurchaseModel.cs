using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class PurchaseModel
    {
        public Nullable<int> QuoteId { get; set; }
        public bool IsImageAttach { get; set; }
        public string CompanyName { get; set; }
        [AllowHtml]
        public string Email { get; set; }
        [AllowHtml]
        public string ClientRef { get; set; }
        public string PONumber { get; set; }
        public string Ref { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public Nullable<System.DateTime> MoveDispatchDate { get; set; }
        public Nullable<System.DateTime> MoveToInvoiceDate { get; set; }
        public Nullable<System.DateTime> MoveToScientistDate { get; set; }
        public int QuoteDetailsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public string LeadTime { get; set; }
        public string CATNo { get; set; }
        public string CATText { get; set; }
        public bool IsUploadServer { get; set; }
        public Guid UniqueId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ProjectType { get; set; }
        public Nullable<int> ScientistCustomerId { get; set; }
        public string RequiredQty { get; set; }
        public int IntRequiredQty { get; set; }
        public Nullable<int> ProjectStatus { get; set; }
        public string ProjectStatustext { get; set; }
        public Nullable<int> ScientistStatus { get; set; }
        public Nullable<int> ReadyToDeliverScientistStatusId { get; set; }
        public string BatchCode1 { get; set; }
        public string Qty1 { get; set; }
        public string BatchCode2 { get; set; }
        public string Qty2 { get; set; }
        public bool? IsOnHold { get; set; }
        public int? DispatchedStatus { get; set; }
        public int ScientistFormCount { get; set; }
        public DateTime? PackDate { get; set; }
        public string PurchaseStatus { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string StrPurchaseDate { get; set; }
        public string ChkRow { get; set; }
        public bool isClubQuote { get; set; }
        public string Courier { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public string RefName { get; set; }
        public string PurposeDispatch { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> ProcessState { get; set; }
        public string DeliveryStatus { get; set; }
        public string DataPending { get; set; }
        public string StrPurchaseDDLStatus { get; set; }
        public string PurchaseDDLStatus { get; set; }
        public string PurchaseRemark { get; set; }
        public Nullable<System.DateTime> EstimateCompleteDate { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public Nullable<bool> MoveToDispatch { get; set; }
        public Nullable<bool> MoveToInvoice { get; set; }
        public string AdminScientistStatus { get; set; }
        public Nullable<int> SrPo { get; set; }
        public string StrEstimateCompleteDate { get; set; }
        public string InvoiceRemark { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentStatus { get; set; }
        public string BatchNo { get; set; }
        public string OrderRemark { get; set; }

        public bool? IsSynthesisLog { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> LastUploadDate { get; set; }

        public string StrPurClientPODate { get; set; }

        public string StrPurOurPODt { get; set; }
        public string StrPurExpectedReceiptDt { get; set; }
        public string StrPurCurrentExpDt { get; set; }
        public string StrPurActualReceiptDt { get; set; }
        public string StrPurTargetDispatchDt { get; set; }
        public string PurPrice { get; set; }
        public string PurSZPONo { get; set; }

        public string ScientistName { get; set; }

        public string AdditionalBatchNoText { get; set; }
        public Nullable<int> AdditionalBatchNo { get; set; }

        public List<SelectListItem> ListBatchNo { get; set; }

        public List<SelectListItem> ListChildCOANo { get; set; }
        public string ScientistStatustext { get; set; }
        public string Productremark { get; set; }
        public string ScientistRemark { get; set; }
        public string SubScientistName { get; set; }
        public int? CompanyId { get; set; }
        public Nullable<bool> IsQueryResolved { get; set; }
        public Nullable<bool> IsPriority { get; set; }
        public string Chemist { get; set; }
        public string APIName { get; set; }
        public string QueryText { get; set; }
        public Nullable<System.DateTime> QueryDate { get; set; }
        public Nullable<bool> IsAssignScientistQuery { get; set; }
        public Nullable<bool> IsAssignProjectQuery { get; set; }
        public Nullable<bool> IsDispatchedLock { get; set; }

        public string DifficultyLevelText { get; set; }
        public string DifficultyLevel { get; set; }

        public string ActivityStatus { get; set; }

        public string LastRaw { get; set; }
        public string CheckboxRaw { get; set; }


        public string EstimateCompleteDateText { get; set; }
        public string MoveToScientistDateText { get; set; }

        public Nullable<bool> IsHoldManually { get; set; }
        public string ReviewSciStatus { get; set; }


        public string PurApprovedStatus { get; set; }
        public string PurMangRemark { get; set; }
        public string TabName { get; set; }


        public Nullable<System.DateTime> PurClientPODate { get; set; }
        public Nullable<System.DateTime> PurOurPODt { get; set; }
        public Nullable<System.DateTime> PurExpectedReceiptDt { get; set; }
        public Nullable<System.DateTime> PurCurrentExpDt { get; set; }
        public Nullable<System.DateTime> PurActualReceiptDt { get; set; }
        public Nullable<System.DateTime> PurTargetDispatchDt { get; set; }
    }
}