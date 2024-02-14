using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Synzeal_Inventory.Entity;

namespace Synzeal_Inventory.Models
{
    public class BDDashboardModel
    {
        public IEnumerable<SZ_QuotationDetail> AssignQuoteDetails { get; set; }

        public IEnumerable<SZ_QuoteDetail_Correctionlog> ConversionData { get; set; }
        public List<SZ_Quotation> RFQQuoteData { get; set; }

        public List<SZ_Quotation> UnderCorrection { get; set; }
        public List<string> SynthesisCatNo { get; set; }

        public int TodayQuotation { get; set; }

        public int TodayApproved { get; set; }
    }
}