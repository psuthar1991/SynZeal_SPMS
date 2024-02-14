using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class DispatchQuoteLogModel
    {
        public int id { get; set; }
        public int? additionalBatch { get; set; }
        public int? coa { get; set; }
        public string activitystatus { get; set; }
    }
}