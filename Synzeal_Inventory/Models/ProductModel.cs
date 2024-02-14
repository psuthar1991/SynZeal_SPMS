using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Gtin { get; set; }
        public string DrugApiCode { get; set; }
        public string MolecularWeight { get; set; }
        public string Synonym { get; set; }
        public string Smile { get; set; }
        public string RefStockPrice {get;set;}
    }
}