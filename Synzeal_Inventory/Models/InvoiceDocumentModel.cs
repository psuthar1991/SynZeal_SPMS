using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class InvoiceDocumentModel
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string CompanyName { get; set; }
        public List<string> Document { get; set; }

        public string Action { get; set; }
    }

}