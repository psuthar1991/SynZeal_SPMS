using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class CompanyModel
    {
        [AllowHtml]
        public string name { get; set; }

        [AllowHtml]
        public string address { get; set; }

        [AllowHtml]
        public string Location { get; set; }

         [AllowHtml]
        public string Branch { get; set; }

        public string masteremail { get; set; }

        public int id { get; set; }

        public string CountryType { get; set; }

        public string UserDistType { get; set; }
        
         public string Country { get; set; }
        public string Contact { get; set; }
        public string FollowupTime { get; set; }
        public string TermsId { get; set; }
        public Nullable<bool> IsPaymentPending { get; set; }
        public Nullable<bool> IsBlockCompany { get; set; }
        public Nullable<bool> IsConversionReport { get; set; }
        public string InccoTerm { get; set; }
        public string PaymentTerms { get; set; }

        [AllowHtml]
        public string Add1 { get; set; }

        [AllowHtml]
        public string Add2 { get; set; }
        [AllowHtml]
        public string City { get; set; }
        [AllowHtml]
        public string State { get; set; }
        [AllowHtml]
        public string PostCode { get; set; }
        [AllowHtml]
        public string ShipAdd1 { get; set; }
        [AllowHtml]
        public string ShipAdd2 { get; set; }
        [AllowHtml]
        public string ShipCity { get; set; }
        [AllowHtml]
        public string ShipState { get; set; }
        [AllowHtml]
        public string ShipCountry { get; set; }

        [AllowHtml]
        public string AnalyticalData { get; set; }

        [AllowHtml]
        public string Telno { get; set; }
        [AllowHtml]
        public string ShipTelno { get; set; }
        [AllowHtml]
        public string ShipPostCode { get; set; }

        
        public int? BDTeam { get; set; }
        
        public int? ProjectTeam { get; set; }

        [AllowHtml]
        public string SAPCode { get; set; }

        [AllowHtml]
        public string ClientSuggestedComment { get; set; }

        public Nullable<int> GeographicalLocation { get; set; }

        public Nullable<bool> IsQuoteHide { get; set; }
        public Nullable<bool> IsApprovedFirst { get; set; }
        
    }
}