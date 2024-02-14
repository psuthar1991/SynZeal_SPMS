using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synzeal_Inventory.Entity;

namespace Synzeal_Inventory.Models
{
    public class DashboardModel
    {
        public List<string> SynthesisCatNo { get; set; }
        public List<SZ_Quotation> Reviewed { get; set; }
        public List<SZ_Quotation> Instock { get; set; }
        public List<SZ_Quotation> CustomSynthesis { get; set; }
        public List<SZ_Quotation> StudyData { get; set; }
        public List<SZ_Quotation> RFQQuoteData { get; set; }
        public List<SZ_Quotation> Approved { get; set; }
        public List<SZ_Quotation> PreApproved { get; set; }
        public List<SZ_Quotation> QuotationList { get; set; }
        public List<MonthWiseUserDashboardDto> UserDashboardModel { get; set; }
        public int TotalOfEmail { get; set; }
        public int TotalOfRegretted { get; set; }
        public int TotalOfEmailCurrent { get; set; }
        public int TotalOfRegrettedCurrent { get; set; }
        public List<UserDashboardSummaryDto> listofquotedetails { get; set; }
        public List<UserDashboardSummaryDto> listofquotedetailsCurrent { get; set; }
        public List<SelectListItem> compList { get; set; }
        public int RFQCount { get; set; }
    }

    public class SystemNotificationModel
    {
        public int Id { get; set; }
        public int SentUserId { get; set; }
        public string SentUsername { get; set; }
        public int ReceivedUserId { get; set; }
        public string ReceivedUserName { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public string CreatedDateText { get; set; }
        public bool IsView { get; set; }
        public int ViewedUserId { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }

        public string ActionRow { get; set; }
    }

    public class ScientistDashboardDataModel
    {
        public int? ScientistId { get; set; }
        public string ScientistName { get; set; }
        public string DiffLevel { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public DateTime Date { get; set; }
    }

    public class ScientistModel
    {
        public string ScientistName {get;set;}
        public int ScientistId {get;set;}

        public int A {get;set;}
        public int B {get;set;}
        public int C {get;set;}
        public int D {get;set;}
        public int E {get;set;}

        public int TotalProduct {get;set;}

        public int Week {get;set;}

        public string ActionRow { get; set; }
    }

    public class ScientistDetailModel
    {
        public string SAPRawMaterial { get; set; }
        public string FirstRow { get; set; }
        public int SrNo { get; set; }
        public string ProStatus { get; set; }
        public int QuoteDetailsId { get; set; }
        public Nullable<int> QuoteId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string CATText { get; set; }

        public string SubScientistName { get; set; }
        public string SubScientistNameText { get; set; }
        public string ActivityStatusText { get; set; }
        public string ActivityStatus { get; set; }
        public string ChemistText { get; set; }
        public string LeadTime { get; set; }
        public string Chemist { get; set; }
        public string Qty { get; set; }

        public string Explaination { get; set; }
        public DateTime? MoveToScientistDate { get; set; }
        public DateTime? OnHoldDate { get; set; }
        public string OnHoldDateText { get; set; }
        public string StatusText { get; set; }
        public string Remark { get; set; }
        public string ProjectType { get; set; }
        public int? Status { get; set; }
        public string AssignDate { get; set; }
        public string EstimateCompDate { get; set; }
        public string SynStartDate { get; set; }
        public string QCApprovedDate { get; set; }
        public string AdditionalBatchNoText { get; set; }
        public string DifficultyLevel { get; set; }
        public string APIName { get; set; }
        public string BatchNo { get; set; }
        public string BatchCode { get; set; }
        
        public string Qty1 { get; set; }
        public string ActionRow { get; set; }
        public string UploadScheme { get; set; }
        public string UploadSchemeEye { get; set; }
        public int IntRequiredQty { get; set; }

        public bool IsPriority { get; set; }

        public bool IsLessQty { get; set; }
    }
    public class ScientistDetailDashboardModel
    {
        public int QuoteId { get; set; }
        public string FirstRaw {get;set;}
        public int QuotationDetailsId { get; set; }
        public int ParentCategoryId { get; set; }
        public string ProductName {get;set;}
        public string ScientistName {get;set;}

        public string SubScientistName {get;set;}
        public string Chemist { get; set; }
        
        public string QuantityDemand {get;set;}
        public string Quantity {get;set;}
        public string DiffLevel {get;set;}
        public string DiffLevelText {get;set;}
        public DateTime SubmitedDate {get;set;}
        public string SubmitedDateText { get; set; }
        public DateTime? ScientistDate {get;set;}

        public DateTime? PODate {get;set;}
        public string Duration {get;set;}
        public string QuotedLeadTime {get;set;}
        public string PONumber {get;set;}
        public string PONumberText { get; set; }
        
        public int? FinalRoute {get;set;}
        public string EarlierSynthesized {get;set;}
        public string PurificationBy {get;set;}
        public string APIcategory {get;set;}
        public string SpeDataAttachment {get;set;}

        public string CASNO {get;set;}
        public string CATNo {get;set;}
        public string CATNoText {get;set;}

        public int Year{ get; set; }
        public int Month{ get; set; }

        public string BatchNo { get; set; }
    }
}