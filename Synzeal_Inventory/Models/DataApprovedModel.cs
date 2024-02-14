using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class DataApprovedModel
    {
        public int Id { get; set; }
        public string ChkSaveRow { get; set; }
        public string Ref { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string PODateText { get; set; }
        public string Remark { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string QTY { get; set; }
        public string DataApprovedStatus { get; set; }
        public string DataApprovedStatusText { get; set; }
         public string AdditionalBatchNoText { get; set; }
        public Nullable<int> AdditionalBatchNo { get; set; }
        public int? ProductId {get;set;}
        public string SelectedAdditionalBatchNo { get;set; }
        public string DataApprovedDateText{ get;set; }
        public Nullable<System.DateTime> DataApprovalDate { get; set; }

        public string ChangeBatch { get; set; }
    }
}