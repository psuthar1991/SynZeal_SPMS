using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ScientistProjectInfoModel
    {
        public int? scStatus { get; set; }
        public string batchcode1 { get; set; }
        public string qty1 { get; set; }
        public string batchcode2 { get; set; }
        public string qty2 { get; set; }
        public int id { get; set; }
        public string esticompleteDate { get; set; }
        public string SynStartDate { get; set; }
        public string remark { get; set; }
        public string subscientist { get; set; }
        public string reason { get; set; }
        public string tabname { get; set; }
        public string chemist { get; set; }
        public string reviewscistatus { get; set; }
    }
}