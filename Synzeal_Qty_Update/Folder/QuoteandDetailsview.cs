//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Qty_Update.Folder
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuoteandDetailsview
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public string LeadTime { get; set; }
        public bool IsUploadServer { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CATNo { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ProjectType { get; set; }
        public Nullable<int> ScientistCustomerId { get; set; }
        public string RequiredQty { get; set; }
        public Nullable<int> ProjectStatus { get; set; }
        public Nullable<int> ScientistStatus { get; set; }
        public string BatchCode1 { get; set; }
        public string Qty1 { get; set; }
        public string BatchCode2 { get; set; }
        public string Qty2 { get; set; }
        public string Courier { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public string RefName { get; set; }
        public string PurposeDispatch { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> DispatchedStatus { get; set; }
        public Nullable<int> ProcessState { get; set; }
        public Nullable<System.DateTime> PackDate { get; set; }
        public string DeliveryStatus { get; set; }
        public string DataPending { get; set; }
        public Nullable<System.DateTime> TrackingNoDate { get; set; }
        public Nullable<System.DateTime> EstimateCompleteDate { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public Nullable<bool> MoveToDispatch { get; set; }
        public string BatchNo { get; set; }
        public Nullable<int> AdditionalBatchNo { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> MoveToInvoice { get; set; }
        public string AdminScientistStatus { get; set; }
        public Nullable<int> SrPo { get; set; }
        public string InvoiceRemark { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentStatus { get; set; }
        public Nullable<bool> IsOnHold { get; set; }
        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public Nullable<System.DateTime> MoveDispatchDate { get; set; }
        public Nullable<System.DateTime> MoveToInvoiceDate { get; set; }
        public Nullable<System.DateTime> MoveToScientistDate { get; set; }
        public string ProductRemark { get; set; }
        public Nullable<bool> IsDataApproved { get; set; }
        public string ScientistRemark { get; set; }
        public string DataApprovedStatus { get; set; }
        public Nullable<System.DateTime> DataApprovalDate { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string PurchaseStatus { get; set; }
        public Nullable<System.DateTime> Instockdate { get; set; }
        public string LastStatus { get; set; }
        public string COAPath { get; set; }
        public string AnalyticalData { get; set; }
        public string ClientRemark { get; set; }
        public string ClientAddress { get; set; }
        public string AttachedDataList { get; set; }
        public string ClientStatus { get; set; }
        public string SubScientistName { get; set; }
        public string FollowUpRemark { get; set; }
        public string FollowUpRemarkSecond { get; set; }
        public Nullable<bool> IsPayment { get; set; }
        public Nullable<bool> IsDispatchApprove { get; set; }
        public Nullable<System.DateTime> EstimateDispatchDate { get; set; }
        public string OrderRemark { get; set; }
        public Nullable<System.DateTime> LastUploadDate { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> ResponseClientRemarkDate { get; set; }
        public Nullable<bool> IsFirstTimeDataUpload { get; set; }
        public string QueryText { get; set; }
        public Nullable<System.DateTime> QueryDate { get; set; }
        public Nullable<bool> IsSynthesisLog { get; set; }
        public Nullable<bool> IsAssignScientistQuery { get; set; }
        public Nullable<bool> IsAssignProjectQuery { get; set; }
        public string Explanation { get; set; }
        public Nullable<System.DateTime> ReportInvoiceDate { get; set; }
        public Nullable<System.DateTime> COAApprovedDate { get; set; }
        public Nullable<bool> IsQueryResolved { get; set; }
        public Nullable<System.DateTime> QueryResolvedDate { get; set; }
        public Nullable<bool> IsDispatchedLock { get; set; }
        public Nullable<bool> IsFollowUpAdminChange { get; set; }
        public Nullable<bool> IsFollowupChange { get; set; }
        public string FollowupDescription { get; set; }
        public string ContactDetail { get; set; }
        public string DifficultyLevel { get; set; }
        public Nullable<bool> IsPriority { get; set; }
        public string FinalPrice { get; set; }
        public string InvoiceBatchNo { get; set; }
        public string QueryType { get; set; }
        public string QuoteBatchNo { get; set; }
        public string ActivityStatus { get; set; }
        public Nullable<System.DateTime> InvoicedDate { get; set; }
        public string BranchLocation { get; set; }
        public string ParkReason { get; set; }
        public string PurchaseDDLStatus { get; set; }
        public string PurchaseRemark { get; set; }
        public string ProStatus { get; set; }
        public string ExplainationSecond { get; set; }
        public string LastWeekUpdate { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> COAId { get; set; }
        public string COARefNumber { get; set; }
        public string PreviousStatus { get; set; }
        public Nullable<System.DateTime> PreviousEstCompleteDate { get; set; }
        public Nullable<bool> IsHoldManually { get; set; }
        public string Chemist { get; set; }
        public string ReviewSciStatus { get; set; }
        public Nullable<bool> IsQC { get; set; }
        public Nullable<System.DateTime> QCdate { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedAs { get; set; }
        public string ApprovalComment { get; set; }
        public string RecommendedPeriod { get; set; }
        public string PrimaryStdOrdered { get; set; }
        public string ColumnOrder { get; set; }
        public string SystemSuitability { get; set; }
        public Nullable<System.DateTime> QCApprovedDate { get; set; }
        public string DispatchStatus { get; set; }
        public string OtherProStatus { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<bool> Expr1 { get; set; }
        public Nullable<bool> IsPaymentPending { get; set; }
        public string Attachment { get; set; }
        public string PaymentTerm { get; set; }
        public bool IsImageAttach { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public string PONo { get; set; }
        public string Ref { get; set; }
        public string QuoteRemark { get; set; }
    }
}
