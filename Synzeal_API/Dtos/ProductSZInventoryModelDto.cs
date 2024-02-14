using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Dtos
{
    public class ProductSZInventoryModelDto
    {
        public int SZInventoryId { get; set; }
        public decimal Qty { get; set; }
        public decimal? AvailableQty { get; set; }
        public string BatchNo { get; set; }
        public string Purity { get; set; }
        public string TGA { get; set; }
        public string Appreance { get; set; }
        public string COA { get; set; }
        public DateTime? ReTestDate { get; set; }
    }
}
