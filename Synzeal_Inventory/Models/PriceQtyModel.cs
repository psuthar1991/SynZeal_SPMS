using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class PriceQtyModel
    {
        public string MG { get; set; }
        public string Price { get; set; }

        public int PackSize { get; set; }
        public decimal Discount { get; set; }
    }
}