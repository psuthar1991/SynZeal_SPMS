using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class RFQQuoteModel
    {
        public int Id { get; set; }
        public string ChkFirstRow { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string Email { get; set; }
        public string RequiredQty { get; set; }
        public DateTime? RFQDate { get; set; }
        public string RFQDateText { get; set; }
        public string Action { get; set; }
    }
}