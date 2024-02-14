using Amazon.Runtime;
using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class QuotationListModel
    {
        public bool FreezeQuote { get; set; }
        public decimal POValue { get; set; }
        public string ProjectAssignName { get; set; }
        public string POCopy { get; set; }
        public string POUrl { get; set; }
        public string APIName { get; set; }
        public string POReceived { get; set; }
        public int QuoteDetailsId { get; set; }
        public int? QuoteId { get; set; }
        public string QuoteDate { get; set; }
        public string LastRow { get; set; }
        public string PONumber { get; set; }
        public string MoveToProjectText { get; set; }
        public string MoveDispatchDateText { get; set; }
        public string DownloadRow { get; set; }
        public string DownloadSampleRow { get; set; }
        public bool? MoveToProject { get; set; }
        public string Currency { get; set; }
        public string Ref { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string LeadTime { get; set; }
        public string ChkFirstRow { get; set; }
        public string ActionRow { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductNameText { get; set; }
        public string PriceText { get; set; }

        public string FinalPriceText { get; set; }
        public string CASNoText { get; set; }
        public string CATNoText { get; set; }
        public Nullable<bool> IsSuccessSAP { get; set; }
        public string CATNoLink { get; set; }
        public string LeadTimeText { get; set; }
        public string PONumberText { get; set; }
        public string PODateText { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string EmailAddress { get; set; }
        public string EmailCC { get; set; }
        public Nullable<int> SrPo { get; set; }
        public string RequiredQty { get; set; }
        public string BatchNo { get; set; }
        public string SelectedAdditionalBatchNo { get; set; }
        public string Courier { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public string RefName { get; set; }

        public string Explanation { get; set; }
        public string ExplainationSecond { get; set; }
        public string PurposeDispatch { get; set; }
        public string DataRemark { get; set; }
        public string MoveToScientistDate { get; set; }

        public string AdminScientistStatus { get; set; }
        public string LastUploadDateText { get; set; }
        public string PackDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsImportedQuote { get; set; }
        public string ProductRemark { get; set; }
        public string EstimateCompleteDate { get; set; }
        public bool IsAlreadInProjectScreen { get; set; }

        public bool IsAlreadInInvoiceScreen { get; set; }
        public Nullable<bool> MoveToDispatch { get; set; }
        public Nullable<bool> MoveToInvoice { get; set; }

        public Nullable<int> DispatchedStatus { get; set; }
        public int? AdditionalBatchNo {get;set;}
        public string AdditionalBatchNoText { get; set; }
        public string DownloadCOAText { get; set; }
        public string COAText { get; set; }
        public int? COAId { get; set; }
        public string ActivityStatus { get; set; }
        public string ActivityStatusText { get; set; }
        public int? InternationStatus { get; set; }
        public string InternationStatusText { get; set; }
        public string SAPSONo { get; set; }
        public int? SAPSOLId { get; set; }
        public string SAPSODocEntry { get; set; }
        public string Feedback { get; set; }
        public string ClientRemark { get; set; }
        public string FollowUpRemark { get; set; }
        public string FollowUpRemarkSecond { get; set; }

        public int? TermsId { get; set; }
        public int? DisplayOrder { get; set; }
        public string ClientRef { get; set; }
        public string ImagePath { get; set; }
        public string Remark { get; set; }
        public bool IsImageAttach { get; set; }
        public string OrderRemark { get; set; }
        public string TechnicalEmail { get; set; }

        public string ColdShipCostRemark { get; set; }
        public string TracebilityCostRemark { get; set; }

        public string ProjectType { get; set; }
        public string ProjectTypeText { get; set; }
        public string SelectedProjectType { get; set; }
        public int? ScientistCustomerId { get; set; }
        public Nullable<System.DateTime> EstimateDispatchDate { get; set; }
        public string EstimateDispatchDateStr { get; set; }
        public string ScientistName { get; set; }
        public string SelectedScientistName { get; set; }
        public Nullable<bool> IsOnHold { get; set; }
        public string FollowUpDescText { get; set; }
        public string AddTestCostRemark { get; set; }
        public Nullable<bool> IsRFQ { get; set; }
        public string ChangeBatch { get; set; }
        
        public string FollowupDescription { get; set; }

        public bool? IsFollowUpAdminChange { get; set; }
        public bool? IsFollowupChange { get; set; }
        public string ContactDetail { get; set; }
        public string TabName { get; set; }

        public string StrProjectStatus { get; set; }

        public int? ProjectStatus { get; set; }
        public string Reason { get; set; }
        public string Attachment { get; set; }
        public string ReasonText { get; set; }
        public string FinalPrice { get; set; }

        public string CountryType { get; set; }

        public string AnalysisDateStr { get; set; }
        public string ReTestDateStr { get; set; }

        public string DispatchStatusText { get; set; }
        public string SelectedDispatchStatus { get; set; }
        public SZ_Quotation SZ_QuotationData { get; set; }
        public SZ_QuotationDetail SZ_QuotationDetailData { get; set; }

        public string ChkSaveRow { get; set; }
        public Nullable<decimal> ItemDiscount { get; set; }
        public string TracebilityCost { get; set; }
        public string ColdShipCost { get; set; }
        public string AddTestCost { get; set; }
        public Nullable<bool> IsControlledSubstance { get; set; }

        public Nullable<bool> IsConversionReport { get; set; }
        public Nullable<int> Discount { get; set; }

        public string FollowupReason { get; set; }
        public string FollowupStatus { get; set; }
        public string FollowupAdminRemark { get; set; }
        public bool IsFollowupLost { get; set; }
        public bool IsFollowupCall { get; set; }
        public bool IsFollowupWon { get; set; }
        public bool IsFollowupInterested { get; set; }
        public bool IsFollowupMail { get; set; }

        public bool IsFollowupMeeting { get; set; }
        public bool IsFollowupLessValue { get; set; }
        public bool IsFollowupNegotiation { get; set; }
        public bool IsFollowupImportant { get; set; }
        

    }
}