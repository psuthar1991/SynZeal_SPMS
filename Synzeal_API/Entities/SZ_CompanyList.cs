using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Entities
{
    public class SZ_CompanyList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MasterEmail { get; set; }
        public string CountryType { get; set; }
        public Nullable<int> TermsId { get; set; }
        public string UserDistType { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string FollowupTime { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Country { get; set; }
        public string PaymentTerms { get; set; }
        public Nullable<bool> IsPaymentPending { get; set; }
        public string Branch { get; set; }
        public Nullable<bool> IsBlockCompany { get; set; }
        public string Add1 { get; set; }
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
        public Nullable<bool> IsConversionReport { get; set; }
        public Nullable<int> BDTeam { get; set; }
        public Nullable<int> ProjectTeam { get; set; }
        public string SAPCode { get; set; }
        public string AnalyticalData { get; set; }

    }
}
