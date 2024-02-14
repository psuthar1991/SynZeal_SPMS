using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class PurchaseRFQModel
    {
        public string ChkSaveRow{ get; set; }
        public int Id { get; set; }
        public string RfqNo { get; set; }
        public Nullable<System.DateTime> AssignedDate { get; set; }

        [AllowHtml]
        public string ChemicalName { get; set; }
        [AllowHtml]
        public string ChemicalNameText { get; set; }

        [AllowHtml]
        public string RefBy { get; set; }

        public string AssignedDateText { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        [AllowHtml]
        public string CASNoText { get; set; }
        [AllowHtml]
        public string CATNoText { get; set; }
        [AllowHtml]
        public string Comment { get; set; }

        public string CommentText { get; set; }
        public string Summary { get; set; }
        public string SummaryText { get; set; }
        [AllowHtml]
        public string PurchaseStatus { get; set; }
        [AllowHtml]
        public string PurchaseStatusText { get; set; }
        [AllowHtml]
        public string PurchaseRemark { get; set; }
        [AllowHtml]
        public string PurchaseRemarkText { get; set; }
        public Nullable<System.DateTime> Estdate { get; set; }

        public string EstdateText { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}