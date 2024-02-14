using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synzeal_Inventory.Entity;

namespace Synzeal_Inventory.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
    }

    public class ClientRemarkModel
    {
        public int Id { get; set; }
        public bool isClientSection { get; set; }
        [AllowHtml]
        public string Remark { get; set; }
    }

    public class PriceDashboardModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int PriceFilledINRCount { get; set; }
        public int PricePendingINRCount { get; set; }

        public int PriceFilledUSDCount { get; set; }
        public int PricePendingUSDCount { get; set; }
        public int ProductCount { get; set; }
    }
}