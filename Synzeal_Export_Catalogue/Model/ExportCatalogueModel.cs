using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_Export_Catalogue.Model
{
    public class ExportCatalogueModel
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Synonym { get; set; }
        public string MolF { get; set; }
        public string MolW { get; set; }
        public string InventoryStatus { get; set; }
        public string ImagePath { get; set; }

        public string CATNo { get; set; }
        public string CASNo { get; set; }
    }
}
