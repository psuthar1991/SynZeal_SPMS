using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ReTestModel
    {
        public int QuoteId { get; set; }
        public int RetestId { get; set; }
        public int BatchId { get; set; }
        public int QuoteDetailId { get; set; }
        public int? ProductId { get; set; }

        public int COAId { get; set; }
        public string FirstRow { get; set; }
        public string ProductName { get; set; }

        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string BatchNo { get; set; }
        public decimal? QTY { get; set; }
        public DateTime? ReTestdate { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public DateTime? QuotationCreateddate { get; set; }
        public string CreatedDateText { get; set; }
        public string ReTestDateText { get; set; }
        public string AnalysisDateText { get; set; }
        public string APIName { get;set;}

        public string RetestRemark { get; set; }
        public string RetestRemarkText { get; set; }

        public string HPLC { get; set; }
        public string TGA { get; set; }

        public string QuotationRef { get; set; }
        public string PONumber { get; set; }
        public DateTime? PONumberDate { get; set; }
        public string PONumberDateText { get; set; }
        public string CompanyName { get; set; }

        public bool ISBatchSelected { get; set; }

        public int Diffmonth { get; set; }

        public string ChangeBatch { get; set; }

        public string LastRow { get; set; }
    }
}