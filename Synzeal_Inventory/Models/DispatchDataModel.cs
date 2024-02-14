using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class DispatchDataModel
    {
        public int id { get; set; }
        public int status { get; set; }
        public string additionalBatch { get; set; }
        public int? sortQty { get; set; }
        public string batchNo { get; set; }
        public string remark { get; set; }
        public string reason { get; set; } = null;
        public string coa { get; set; } = null;
        public string orderremark { get; set; } = null;
        public string tabname { get; set; } = null;
    }


    public class DispatchSummaryModel
    {
        public string BatchNo { get; set; }
        public string PONo { get; set; }
        public string CompanyName { get; set; }
        public string TeamLeader { get; set; }
        public string ProductName { get; set; }
        public string ProjectStatus { get; set; }
        public string Qty { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string DispatchedDate { get; set; }
        public string BatchRecord { get; set; }
        public string QueryReceived { get; set; }
    }
}