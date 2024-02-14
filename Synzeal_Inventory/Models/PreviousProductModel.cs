using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class PreviousProductModel
    {
        public string QuoteDate { get; set; }
        public string QuoteRef { get; set; }
        public string CompanyName { get; set; }
        public string PONo { get; set; }

        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string Price { get; set; }
        public string FinalPrice { get; set; }
        public string CopyProduct { get; set; }
        public string RequiredQty { get; set; }
        public string LeadTime { get; set; }
        public string CountryType { get; set; }
        public string ProductRemark { get; set; }
        public string Location { get; set; }
        public string ApprovedBy { get; set; }

    }
}