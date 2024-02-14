using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class SZ_PerformaInvoiceModelcs
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PerformaInvoiceNo { get; set; }
        public string CustomerPoNo { get; set; }
        public DateTime PerformaInvoiceDate { get; set; }
        public string Currency { get; set; }
        public string PaymentTerm { get; set; }

        [AllowHtml]
        public string BillCompanyName { get; set; }
        public bool SaveAndPdf { get; set; }
        [AllowHtml]
        public string BillAddress { get; set; }
        public string BillCountry { get; set; }
        public string BillTelno { get; set; }
        [AllowHtml]
        public string ShipCompanyName { get; set; }
        [AllowHtml]
        public string ShipAddress { get; set; }
        public string ShipCountry { get; set; }
        public string ShipTelno { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public string HSNCode { get; set; }
        public string Courier { get; set; }
        public int BillAddressId { get; set; }
        public int ShipAddressId { get; set; }
        [AllowHtml]
        public string PortOfDischarge { get; set; }
        [AllowHtml]
        public string Incoterm { get; set; }

        public string ShippingCost { get; set; }

        public Nullable<bool> IsIGST { get; set; }
        public string BillGSTNo { get; set; }
        public string ShipGSTNo { get; set; }
    }
}