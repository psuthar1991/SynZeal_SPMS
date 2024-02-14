using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class SuggestedPriceModel
    {
        public DateTime? QuotedDate { get; set; }
        public int QuoteId { get; set; }
        public int QuotationDetailsId { get; set; }
        public List<SuggestedPriceListModel> PriceList { get; set; }
    }

    public class SuggestedPriceListModel
    {
        public int MG { get; set; }
        public int Price { get; set; }
    }

    public class PriceMasterRange
    {
        public int StartRange { get; set; }
        public int EndRange { get; set; }
    }

    public class SuggestedPriceListQuoteModel
    {
        public int id { get; set; }
        public int quoteid { get; set; }

        public string qty { get; set; }
    }
    public class MasterPriceQuoteModel
    {
        public string catno { get; set; }
        public string countrytype { get; set; }
        public string qty { get; set; }
    }

    public class CorrectionLogModel
    {
        public int quoteid { get; set; }
        public int quotedetailsId { get; set; }

        [AllowHtml]
        public string correctionLog { get; set; }
    }
}