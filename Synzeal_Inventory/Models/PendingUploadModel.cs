using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class PendingUploadModel
    {
        public string FirstColumn { get; set; }
        public Nullable<int> QuoteId { get; set; }
        [AllowHtml]
        public string Ref { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> MoveProjectDate { get; set; }
        public int QuoteDetailsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string CASNoText { get; set; }
        public string CATNoText { get; set; }
        public Nullable<bool> MoveToProject { get; set; }
        public bool? IsOnHold { get; set; }

        public string AdditionalBatchNoText { get; set; }

        public string CreatedbyText { get; set; }
        public string AttachmentText { get; set; }

    }
}