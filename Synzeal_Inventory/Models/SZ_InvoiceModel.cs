using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SZ_InvoiceModel
    {
        public int QuoteId { get; set; }
        public int CompanyId { get; set; }
        public string TrackingNo { get; set; }
        public string CompanyName { get; set; }
        public string InvoiceNo { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string TermsId { get; set; }
        public string Add1 { get; set; }
        public string PONo { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string ShipAdd1 { get; set; }
        public string ShipAdd2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public string Telno { get; set; }
        public string ShipTelno { get; set; }
        public string ShipPostCode { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public int? ShippingCost { get; set; }
        public string Temperature { get; set; }
        public string GrossWeight { get; set; }
        public List<SZInvoiceQuoteDetailsModel> QuoteDetailsModel { get; set; }
    }

    public class SZInvoiceQuoteDetailsModel
    {
        public string Rate { get; set; }
        public int QuoteDetailId { get; set; }
        public int Qty { get; set; }

        public string HSN { get; set; }
        public string ADCQty { get; set; }
        public string DSLNo { get; set; }
        public string FOB { get; set; }
        public string Remark { get; set; }
        public string Total { get; set; }
    }
}