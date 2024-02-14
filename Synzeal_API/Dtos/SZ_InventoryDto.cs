﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Entities
{
    public class SZ_InventoryDto
    {
        public SZ_InventoryDto()
        {
        }
        public int SZInventoryId { get; set; }
        public decimal Qty { get; set; }
        public decimal? AvailableQty { get; set; }
        public string BatchNo { get; set; }
        public string Purity { get; set; }
        public string TGA { get; set; }
        public string Appreance { get; set; }
        public string COA { get; set; }
        public DateTime? ReTestDate { get; set; }
    }


    public class MovetoProjectModel
    {
        public Nullable<int> CompanyId { get; set; }
        public string SAPCode { get; set; }
        public int QuoteId { get; set; }
        public string QuoteRef { get; set; }
        public int QuoteDetailId { get; set; }
        public string PONo { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
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
        public string ScientistCustomerName { get; set; }
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

        public string LastExplainationSecond { get; set; }
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
        public Nullable<bool> IsPurchase { get; set; }
        public Nullable<System.DateTime> QuotePurchaseDate { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string CancelledBy { get; set; }
        public Nullable<bool> ApprovedForClient { get; set; }
        public Nullable<bool> IsBackFromPurchase { get; set; }
        public Nullable<bool> IsForceUpload { get; set; }
        public string InhouseRemark { get; set; }
        public string LastProStatus { get; set; }
        public Nullable<System.DateTime> QcApprovalDate { get; set; }
    }


    public class MovetoProjectSAPModel
    {
        public int SAPSOLId { get; set; }
        public int SAPSODocEntry { get; set; }
        public int SAPSONo { get; set; }
        public string Reason { get; set; }
        public string OrderRemark { get; set; }
        public string ActivityStatus { get; set; }
        public string Projectstatus { get; set; }    
        public string sapCusName { get; set; }
        public string PONo { get; set; }
        public string SAPCusCode { get; set; }
        public string DocDate { get; set; }
        public int QuoteId { get; set; }
        public int QuoteDetailId { get; set; }
        public string DocType { get; set; }
        public string DocCurrency { get; set; }
        public string DocRemark { get; set; }
        public string ItemCode { get; set; }
        public string LineRemark { get; set; }
        public string CASNo { get; set; }
        public string ItemName { get; set; }
        public decimal QuantityPO { get; set; }
        public string LineType { get; set; }
        public decimal PriceBfrDiscount { get; set; }
        public decimal PriceAfrDiscount { get; set; }
        public decimal Discount { get; set; }
        public int PackSize { get; set; }
        public int NoofPack { get; set; }
        public string BatchNo { get; set; }
        public string COARefNumber { get; set; }
        public decimal UnitPerRate { get; set; }
        public bool IsInvoice { get; set; }
    }
}
