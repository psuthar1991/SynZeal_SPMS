using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_SAP_Stock_API
{
    public class SZ_InventoryModel
    {
        public string Batch { get; set; }
        public string BatchQty { get; set; }
        public string BinLocation { get; set; }
        public string CASNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string UomCode { get; set; }
        public string WhsCode { get; set; }
    }
}
