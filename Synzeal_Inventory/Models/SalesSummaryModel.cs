using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
	public class SalesSummaryModel
	{
        public List<SalesSummaryDataModel> TodayData { get; set; }
        public List<SalesSummaryDataModel> WeekData { get; set; }
        public List<SalesSummaryDataModel> MonthData { get; set; }
        public List<SalesSummaryDataModel> YearData { get; set; }

    }

    public class SalesSummaryDataModel
    {
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public int RFQProducts { get; set; }
        public int QuoteProducts { get; set; }
        public string Conversion { get; set; }
        public int Total { get; set; }
    }

    public class WeeklySummaryModel
    {
        public string Company { get; set; }
        public string QuoteID { get; set; }
        public string Product { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public int QuantityQuoted { get; set; }
        public string LeadTime { get; set; }
        public string ProductRemarks { get; set; }
        public string ExportDomestic { get; set; }

    }
}