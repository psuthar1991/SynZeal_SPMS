using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class SZ_QuotationModel
    {
        public Nullable<decimal> ShippingCharges { get; set; }

        public Nullable<int> QuoteId { get; set; }
        [AllowHtml]
        public string Ref { get; set; }
        public Nullable<int> CompanyId { get; set; }
        
        public Guid UniqueId { get; set; }
        public string CompanyName { get; set; }
        [AllowHtml]
        public string Email { get; set; }

        [AllowHtml]
        public string ClientRef { get; set; }
        public string PONumber { get; set; }
        public bool IsImageAttach { get; set; }
        public bool IsToBe { get; set; }
        public bool IsQuoteApproved { get; set; }
        public bool Auction { get; set; }
        
        public bool SaveAndPdf { get; set; }
        public string Remark { get; set; }
        public string CountryType{ get; set; }
        public string UserDistType { get; set; }

        public string LayoutType { get; set; }
        public int TermsId { get; set; }
        public bool IsClubQuotation { get; set; }
        public Nullable<bool> IsCOA { get; set; }
        public Nullable<DateTime> EmailReceivedDate { get; set; }
        public Nullable<DateTime> PODate { get; set; }
        [AllowHtml]
        public string SuggChemName { get; set; }
        [AllowHtml]
        public string Attachment { get; set; }
        public bool SendMail { get; set; }
        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public Nullable<System.DateTime> MoveDispatchDate { get; set; }
        public Nullable<System.DateTime> MoveToInvoiceDate { get; set; }
        public Nullable<System.DateTime> MoveToScientistDate { get; set; }
        public List<SZ_QuotationProductModel> SZ_QuotationProductModel { get; set; }
        [AllowHtml]
        public string EmailCC { get; set; }

        public bool?  IsFollowupRequired { get; set; }
        public Nullable<bool> IsPreviewed { get; set; }
        public Nullable<bool> IsReviewed { get; set; }
        public Nullable<bool> IsInstock { get; set; }
        public Nullable<bool> IsCustomSynthesis { get; set; }
        [AllowHtml]
        public string QuoteComment { get; set; }

        [AllowHtml]
        public string ExternalComment { get; set; }
        public string PaymentTerm { get; set; }
        public string IncoTerm { get; set; }
        public Nullable<bool> IsShippedCharge { get; set; }
        public Nullable<bool> IsShowDashboard { get; set; }
        public Nullable<bool> IsAnalyticalData { get; set; }
        public Nullable<bool> IsStudy { get; set; }
        public Nullable<bool> IsRegret { get; set; }

        public Nullable<bool> IsRevision { get; set; }
        public Nullable<bool> IsPreApproved { get; set; }

        public string PurchaseName { get; set; }
        public string PurchaseContactNo { get; set; }
        public string PurchaseEmail { get; set; }
        [AllowHtml]
        public string PurchaseAddress { get; set; }
        public string PurchaseCity { get; set; }
        public string PurchaseCountry { get; set; }
        public string TechnicalName { get; set; }
        public string TechnicalContactNo { get; set; }
        public string TechnicalEmail { get; set; }
        [AllowHtml]
        public string TechnicalAddress { get; set; }
        public string TechnicalCity { get; set; }
        public string TechnicalCountry { get; set; }
        public string Currency { get; set; }
        [AllowHtml]
        public string QuotePDF { get; set; }

        public Nullable<bool> IsRFQManually { get; set; }
        public Nullable<int> AssignTo { get; set; }
        public Nullable<bool> IsUnderCorrection { get; set; }
        public Nullable<int> HelpScoatNumber { get; set; }
        public int? DocumentTypeId { get; set; }
        public Nullable<bool> IsCustomPDFLayout { get; set; }

        public string Url { get; set; }
        public string POUrl { get; set; }
        
        public string ConversionNumber { get; set; }
        public string FollowupAdminRemark { get; set; }

        public Nullable<int> ReminderCount { get; set; }
    }

    public class SZ_QuotationProductModel
    {
        public int QuoteDetailsId { get; set; }
        public Nullable<int> QuoteId { get; set; }
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

        public string PurchaseDDLStatus{ get; set; }
        public string PurchaseRemark{ get; set; }
        public Nullable<System.DateTime> EstimateCompleteDate { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public Nullable<bool> MoveToDispatch { get; set; }
        public Nullable<bool> MoveToInvoice { get; set; }
        public string AdminScientistStatus { get; set; }
        public Nullable<int> SrPo { get; set; }
        public string InvoiceRemark { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentStatus { get; set; }
        public string BatchNo { get; set; }
        public string Remark { get; set; }
        public string OrderRemark { get; set; }
        
        public bool? IsSynthesisLog { get; set; }
        public string Reason { get; set; }

        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public Nullable<System.DateTime> MoveDispatchDate { get; set; }
        public Nullable<System.DateTime> MoveToInvoiceDate { get; set; }
        public Nullable<System.DateTime> MoveToScientistDate { get; set; }
        public Nullable<System.DateTime> LastUploadDate { get; set; }
        

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

        public string PurPrice { get; set; }
        public string PurSZPONo { get; set; }
    }
}