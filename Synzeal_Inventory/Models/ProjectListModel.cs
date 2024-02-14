using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class CompanyListModel
    {
        public int  Id { get; set; }
        public string SAPCode { get; set; }
        public string Name { get; set; }
        public string CountryType { get; set; }
        public string UserDist { get; set; }
        public string Action { get; set; }
        public bool IsBlockCompany { get; set; }
        public string Firstrow { get; set; }
    }
        public class ProjectListModel
    {
        public int ImportantDays { get; set; }
        public string PrevBatchDetail { get; set; }
        
        public string AnalysisCompletionDate { get; set; }
        
        public string ReceivedFrom { get; set; }
        public int srNo { get; set; }
        public string ReactionMatrix { get; set; }
        public string MSDS { get; set; }
        public string ProjectAssignName { get; set; }
        public string GLP { get; set; }
        public string GLPRemark { get; set; }
        public string GLPStatus { get; set; }
        public Nullable<System.DateTime> GLPAssignDate { get; set; }
        public string GLPAssignDateText { get; set; }
        public string GLPStabilityPath { get; set; }
        public string GLPAMVPath { get; set; }
        public bool? IsConversionReport { get; set; }
        public string SAPCode { get; set; }
        public int? CountEstDate { get; set; }
        public string ChkSaveRow { get; set; }
        public string ApprovedFormStatus { get; set; }
        public string ChkFirstRow { get; set; }
        public Nullable<int> QuoteId { get; set; }
        public string Ref { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Guid UniqueId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string SynStartDate { get; set; }

        public bool IsImageAttach { get; set; }
        public bool SaveAndPdf { get; set; }
        public string Remark { get; set; }
        public string RemarkText { get; set; }

        public int TermsId { get; set; }
        public bool IsClubQuotation { get; set; }
        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public Nullable<System.DateTime> MoveDispatchDate { get; set; }
        public Nullable<System.DateTime> MoveToInvoiceDate { get; set; }
        public Nullable<System.DateTime> MoveToScientistDate { get; set; }
        public Nullable<System.DateTime> QueryDate { get; set; }
        
        public string MoveProjectDateText { get; set; }

        public string InvoicedDateText { get; set; }
        public int QuoteDetailsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public string LeadTime { get; set; }
        public string CATNo { get; set; }
        public bool IsUploadServer { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ProjectType { get; set; }
        public Nullable<bool> ApprovedForClient { get; set; }
        public string ApprovedForClientText { get; set; }
        public string SelectedProjectType { get; set; }
        public string ProjectTypeText { get; set; }
        public Nullable<int> SubScientistCustomerId { get; set; }
        public Nullable<int> ScientistCustomerId { get; set; }

        public Nullable<bool> IsPaymentPending { get; set; }
        public string RequiredQty { get; set; }
        public Nullable<int> ProjectStatus { get; set; }
        public string ProjectStatustext { get; set; }
        public Nullable<int> ScientistStatus { get; set; }
        public string ScientistStatustext { get; set; }
        public string LastStatusText { get; set; }
        public Nullable<int> ReadyToDeliverScientistStatusId { get; set; }
        public string BatchCode1 { get; set; }
        public string Qty1 { get; set; }
        public string BatchCode2 { get; set; }
        public string Qty2 { get; set; }
        public bool? IsOnHold { get; set; }
        public int? DispatchedStatus { get; set; }

        public DateTime? PackDate { get; set; }

        public string Courier { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public string RefName { get; set; }
        public string PurposeDispatch { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> ProcessState { get; set; }
        public string DeliveryStatus { get; set; }
        public string DataPending { get; set; }
        public Nullable<System.DateTime> EstimateCompleteDate { get; set; }
        public Nullable<System.DateTime> PreviousEstCompleteDate { get; set; }
        public string EstimateCompleteDateText { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public Nullable<bool> MoveToDispatch { get; set; }
        public Nullable<bool> MoveToInvoice { get; set; }
        public string AdminScientistStatus { get; set; }
        public string LastStatus { get; set; }
        public Nullable<int> SrPo { get; set; }
        public string InvoiceRemark { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentStatus { get; set; }
        public string BatchNo { get; set; }
        public string BatchNoText { get; set; }
        public string SelectedAdditionalBatchNo { get; set; }

        public string ApprovalCommentsText { get; set; }
        public string AdditionalBatchNoText { get; set; }
        public Nullable<bool> IsControlledSubstance { get; set; }
        public string ProductScheme { get; set; }

        public string SelectedDispatchStatus { get; set; }
        public string DispatchStatusText { get; set; }

        public string ScientistName { get; set; }
        public string SelectedScientistName { get; set; }

        public Nullable<int> AdditionalBatchNo { get; set; }
        public string LastRowText { get; set; }
        public string RequiredQtyTxt { get; set; }
        public string ScientistRemark { get; set; }
        public string PurchaseStatusText { get; set; }
        public string ClientStatusText { get; set; }
        public string ClientStatus { get; set; }
        public string COADownloadLink { get; set; }
        public string AnalyticalDataLink { get; set; }
        public string AttachedDataList { get; set; }

        public string ClientRemarkText { get; set; }
        public string ChangeBatch { get; set; }
        
        public string ClientAddressText { get; set; }
        public string ClientRemark { get; set; }
        public string ClientAddress{ get; set; }
        public string SubScientistName { get; set; }
        public string SelectedSubScientistName { get; set; }
        public string COAText { get; set; }

        public string GetAllBatch { get; set; }
        public bool? IsDispatchApprove { get; set; }

        public string IsPaymentText { get; set; }
        public bool? IsPayment { get; set; }

        public string OrderRemarkText { get; set; }
        public string OrderRemark { get; set; }

        public string ReasonText { get; set; }
        public string Reason { get; set; }
        public string SAPRawMaterial { get; set; }
        public string SuggChemName { get; set; }
        public string Attachment { get; set; }
        public string CountryType { get; set; }
        public string LastUploadDateStr { get; set; }

        public string MoveToScientistDateStr { get; set; }
        public Nullable<bool> IsAssignScientistQuery { get; set; }
        public Nullable<bool> IsAssignProjectQuery { get; set; }

        public string ReportInvoiceDateText { get; set; }
        public Nullable<System.DateTime> ReportInvoiceDate { get; set; }
        public Nullable<System.DateTime> COAApprovedDate { get; set; }

        public string ClientChat { get; set; }
        public string ExplainationText { get; set; }
        public string InhouseRemarkText { get; set; }
        public string ResolvedQueryText { get; set; }
        public Nullable<bool> IsQueryResolved { get; set; }

        public string DifficultyLevel { get; set; }
        public string DifficultyLevelText { get; set; }

        public string IsPriorityText { get; set; }
        public bool? IsPriority { get; set; }

        public int? ProductCount { get; set; }

        public int? TotalRecord { get; set; }
        public Nullable<bool> IsCancel { get; set; }

        public string ActivityStatus { get; set; }
        public string ActivityStatusText { get; set; }

        public string TechnicalEmail { get; set; }

        public string APIName { get; set; }

        public string PurchaseDDLStatusText { get; set; }
        public string PurchaseDDLStatus { get; set; }

        public string PurchaseRemarkText { get; set; }
        public string PurchaseRemark { get; set; }

        public string PurchaseDateText { get; set; }

        public Nullable<System.DateTime> PurchaseDate { get; set; }

        public string ProStatus { get; set; }
        public string PreviousStatus { get; set; }
        public string ProStatusText { get; set; }
        public string PreviousStatusText { get; set; }
        public string SelectedProStatus { get; set; }

        public string ExplainationSecond{ get; set; }
        public string ExplainationSecondText{ get; set; }
        public string LastWeekUpdate { get; set; }
        public string PaymentTerms { get; set; }
        public Nullable<int> COAId { get; set; }
         public string COARefNumber { get; set; }
        public string OtherProStatusText { get; set; }
        public string SelectedOtherProStatus { get; set; }
        public string SpectralData { get; set; }
        public int? LicesenStatusId { get; set; }
        public string LicesenStatusText { get; set; }
        public string PermitRequired { get; set; }
        public string ImportPermit { get; set; }
        public string Declaration { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? ExportPermitReceivedDate { get; set; }
        public string ApplicationDateText { get; set; }
        public string ExportPermitReceivedDateText { get; set; }
        public string TechnicalWriteup { get; set; }
        public string QuaterlyDataToSubmit { get; set; }
        public string QuaterlyDataSubmited { get; set; }
        public string NextQuater { get; set; }
        public string JourneyComment { get; set; }
        public string APIRequired { get; set; }
        public string APIImpExport { get; set; }
        public string Category { get; set; }
        public string ActionRow { get; set; }

        public string ImpExpo { get; set; }

    }
}