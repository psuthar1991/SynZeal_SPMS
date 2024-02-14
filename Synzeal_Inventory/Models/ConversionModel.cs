using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ConversionModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int QuotedProduct { get; set; }
        public int POProduct { get; set; }
        public decimal Conversion { get; set; }
    }
}