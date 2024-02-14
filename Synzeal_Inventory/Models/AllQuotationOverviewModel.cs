using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class AllQuotationOverviewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int AllQuote { get; set; }
        public int AllPO { get; set; }
        public int AllHold { get; set; }
    }
}