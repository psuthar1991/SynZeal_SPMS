using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class MovetoprojectModel
    {
        public List<int> qdids { get; set; }
        public string bName { get; set; }
        public int compId { get; set; }
        public List<int> isOnHold { get; set; }
        public string popPurchaseName { get; set; }
        public string popPurchaseEmail { get; set; }
        public string popPurchaseContactNo { get; set; }
        public string popPurchaseAddress { get; set; }
        public string popPurchaseCity { get; set; }
        public string popPurchaseCountry { get; set; }
        public string popTechnicalEmail { get; set; }
        public string popTechnicalContactNo { get; set; }
        public string popTechnicalAddress { get; set; }
        public string popTechnicalCity { get; set; }
        public string popTechnicalCountry { get; set; }
        public DateTime? popuppodate { get; set; }
        public string popuppourl { get; set; }
    }
}