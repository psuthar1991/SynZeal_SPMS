using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DocLine
    {
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string CASNo { get; set; }
        public decimal Quantity { get; set; }
        public string UOMCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public string WarehouseCode { get; set; }
        public string ProjectCode { get; set; }
        public int PackSize { get; set; }
        public double NoPack { get; set; }
        public string ActivityStatus { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectType { get; set; }
        public string LineRemark { get; set; }
        public object BatchNo { get; set; }
        public string COARefNumber { get; set; }
        public string Reason { get; set; }
        public string DataRemark { get; set; }
        public string QuoteId { get; set; }
        public string QuoteLId { get; set; }
        public string ExtPK_LId { get; set; }
        public string ExtPK_LType { get; set; }
        public string ExtPK { get; set; }
    }

    public class SAPPostModel
    {
        public List<DocLine> Doc_Lines { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public object CustomerRefNo { get; set; }
        public string DocCurrency { get; set; }
        public string BPCode { get; set; }
        public string BPName { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public string DocRemark { get; set; }
        public string DocRemark_Opening { get; set; }
        public string DocRemark_Closing { get; set; }
        public string ExternalPK { get; set; }
        public string EmpCode { get; set; }
        public string ApplicationType { get; set; }
        public string TransactionType { get; set; }
    }


}