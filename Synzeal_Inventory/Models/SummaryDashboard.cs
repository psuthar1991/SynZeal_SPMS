using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SummaryDashboard
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int TotalQuote { get; set; }
        public int TotalProduct { get; set; }
        public int TotalProductValue { get; set; } 
        public int TotalPO { get; set; }
        public int TotalPOProduct { get; set; }
        public int TotalPOValue { get; set; }
        public decimal PercentageConvert { get; set; }
        public string ActionRow { get; set; }
    }
}