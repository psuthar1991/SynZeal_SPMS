using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_BatchFormData_Sync.Model
{
    public class SZ_InventoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string BatchNo { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Appearance { get; set; }
        public string COAPath { get; set; }
        public string StdDataPath { get; set; }
        public string AddDataPath { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ReTestDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<decimal> DeepQty { get; set; }
        public Nullable<decimal> RegularQty { get; set; }
        public string ReTestRemark { get; set; }
        public Nullable<decimal> ReservedQty { get; set; }
        public Nullable<decimal> AvailableQty { get; set; }
    }
}
