using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class MovetoProjectSAPModel
    {
        public int SAPSOLId { get; set; }
        public int SAPSODocEntry { get; set; }
        public int SAPSONo { get; set; }
        public string Reason { get; set; }
        public string OrderRemark { get; set; }
        public string ActivityStatus { get; set; }
        public string Projectstatus { get; set; }
        public string sapCusName { get; set; }
        public string PONo { get; set; }
        public string SAPCusCode { get; set; }
        public string DocDate { get; set; }
        public int QuoteId { get; set; }
        public int QuoteDetailId { get; set; }
        public string DocType { get; set; }
        public string DocCurrency { get; set; }
        public string DocRemark { get; set; }
        public string ItemCode { get; set; }
        public string LineRemark { get; set; }
        public string CASNo { get; set; }
        public string ItemName { get; set; }
        public decimal QuantityPO { get; set; }
        public string LineType { get; set; }
        public decimal PriceBfrDiscount { get; set; }
        public decimal PriceAfrDiscount { get; set; }
        public decimal Discount { get; set; }
        public int PackSize { get; set; }
        public int NoofPack { get; set; }
        public string BatchNo { get; set; }
        public string COARefNumber { get; set; }
        public decimal UnitPerRate { get; set; }
        public bool IsInvoice { get; set; }
    }
}