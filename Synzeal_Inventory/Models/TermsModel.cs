using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class TermsModel
    {
        public int QuoteId { get; set; }
        public int Id { get; set; }
        [AllowHtml]
        public string Message { get; set; }
    }

    public class ExportDispatchModel
    {
        public string ProductName { get; set; }

        public string Qty { get; set; }

        public string CASNo { get; set; }

        public string CATNo { get; set; }

        public string BatchNo { get; set; }

        public string Barcode { get; set; }

        public string PONo { get; set; }
    }
}