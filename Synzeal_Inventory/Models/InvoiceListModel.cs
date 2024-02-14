using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class InvoiceListModel
    {
        public bool? IsControlledSubstance { get; set; }
        
        public bool? IsConversionReport { get; set; }
        public string Courier { get; set; }
        public string DeliveryStatus { get; set; }
        public string DataPending { get; set; }
        public string PaymentStatus { get; set; }
        public string PurposeDispatch { get; set; }

        public string Remark { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceRemark { get; set; }
        public string RefName { get; set; }
        public string PackDate { get; set; }
        public string InvoicedDate { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public string ProductName { get; set; }
        public string RequiredQty { get; set; }
        public string BatchNo { get; set; }
        public string LeadTime { get; set; }
        public int QuoteDetailsId { get; set; }
        public string ProductRemark { get; set; }
        public string Price { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string Ref { get; set; }
        public string CompanyName { get; set; }
        public string PONo { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<bool> IsImportedQuote { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string EmailAddress { get; set; }
        public int QuoteId { get; set; }
        public Nullable<int> TotalRecord { get; set; }
        public string DeliveryDateStr { get; set; }
        public string FirstRow { get; set; }
        public string SecondRow { get; set; }
        public string ThirdRow { get; set; }
        public string OrderRemark { get; set; }
        public string TrackingNoStr { get; set; }
        public string LocationStr { get; set; }
        public string RefNameStr { get; set; }
        public string DataPendingStr { get; set; }
        public string InvoiceRemarkStr { get; set; }
        public string InvoiceNoStr { get; set; }
        public string DeliveryStatusStr { get; set; }
        public string PaymentStatusStr { get; set; }
        public string PurposeDispatchStr { get; set; }
        public string CourierStr { get; set; }
        public string InvoiceBatchNo { get; set; }
        public Nullable<int> ProductCount { get; set; }
        public string InvoicePDF { get; set; }
        public string COARefNumber { get; set; }
        public int? PaymentDays { get; set; }
        public bool IsPaymentExpired { get; set; }
        public string COAPath { get; set; }
        public string AnalyticalData { get; set; }
    }

    public class OverviewSciDashboard
    {
        public string RowName { get; set; }
        public string CoumnName { get; set; }
        public int ProductCount { get; set; }
    }

    public class InvoiceInformationModel
    {
        public string courier { get; set; }
        public string tracking { get; set; }
        public string location { get; set; }
        public string refno { get; set; }
        public string purposeofdispatch { get; set; }
        public string deliverydate { get; set; }
        public string deliverystatus { get; set; }
        public string datapending { get; set; }
        public int id { get; set; }
        public string remark { get; set; }
        public string paymentStatus { get; set; }
        public string invoiceno { get; set; }
    }

    public class OverviewQuotationCountModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int Total { get; set; }
        public int POCount { get; set; }
        public int HoldCount { get; set; }
        public int DispatchCount { get; set; }
        
    }
}