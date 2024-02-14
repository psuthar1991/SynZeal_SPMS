using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ProCatModel
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int ParentCategoryid { get; set; }
        public string CategoryName { get; set; }
    }

    public class ChartModel
    {
        public string LabelName { get; set; }
        public int Count { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}