using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ExportInvoiceModel
    {
        public string APIName { get; set; }
        public int? ProductId { get; set; }
         public string CountryType { get; set; }
        public string BatchNo { get; set; }
        public string Courier { get; set; }
        public string DeliveryStatus { get; set; }
        public string DataPending { get; set; }
        public string PaymentStatus { get; set; }
        public string PurposeDispatch { get; set; }
        public string TrackingNo { get; set; }
        public string Location { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceRemark { get; set; }
        public string RefName { get; set; }
        public DateTime? PackDate { get; set; }
        public string ProductName { get; set; }
        public string RequiredQty { get; set; }
        public string LeadTime { get; set; }
        public string ProductRemark { get; set; }
        public string Price { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string Ref { get; set; }
        public string CompanyName { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }
        public string OrderRemark { get; set; }
        public string COARefNumber { get; set; }
    }
}