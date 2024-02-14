using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class PriceMatchStatusModel
    {
        public int id { get; set; }
        public int mg { get; set; }
        public int price { get; set; }
        public string catno { get; set; }
        public string countryType { get; set; }
        public bool isDown { get; set; }
        public bool isUp { get; set; }
        public bool isSame { get; set; }
        public int diff { get; set; }
        public int LastToLastPrice { get; set; }
        public int type { get; set; }
        public string timeAgo { get; set; }
        public string currancy { get; set; }
        public bool IsMovetoProject { get; set; }
    }
}