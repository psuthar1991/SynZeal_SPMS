using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SAPPaymentTerm
    {
        public int SrNo { get; set; }
        public int PaymentTermsCode { get; set; }
        public string PaymentTermsName { get; set; }
    }

    public class SAPIncoTerm
    {
        public string INCO_TERM { get; set; }
    }

    public class MyArray
    {
        public int SrNo { get; set; }
        public string CardType { get; set; }
        public string BPCode { get; set; }
        public string BPName { get; set; }

        [JsonProperty("BP Group")]
        public string BPGroup { get; set; }

        [JsonProperty("Conatct Person")]
        public string ConatctPerson { get; set; }

        [JsonProperty("Web Site")]
        public string WebSite { get; set; }
        public string EMail { get; set; }
        public string Mobile { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int PaymentTermsCode { get; set; }
        public string PaymentTermsName { get; set; }
        public string IncoTerms { get; set; }
        public string Bill2_Block { get; set; }
        public string Bill2_Building { get; set; }
        public string Bill2_Address { get; set; }
        public string Bill2_StreetNo { get; set; }
        public string Bill2_City { get; set; }
        public string Bill2_ZipCode { get; set; }
        public string Bill2_State { get; set; }
        public string Bill2_StateName { get; set; }
        public string Bill2_Country { get; set; }
        public string Bill2_CountryName { get; set; }
        public string Ship2_Block { get; set; }
        public string Ship2_Building { get; set; }
        public string Ship2_Address { get; set; }
        public string Ship2_StreetNo { get; set; }
        public string Ship2_City { get; set; }
        public string Ship2_ZipCode { get; set; }
        public string Ship2_State { get; set; }
        public string Ship2_StateName { get; set; }
        public string Ship2_Country { get; set; }
        public string Ship2_CountryName { get; set; }
    }

    public class CompanyMasterSAP
    {
        public List<MyArray> MyArray { get; set; }
    }
}