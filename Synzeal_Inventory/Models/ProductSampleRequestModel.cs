using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class ProductSampleRequestModel
    {
        public int Id { get; set; }
        public string RequestNo { get; set; }
        public string Name { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string Department { get; set; }
        public int ProductId { get; set; }
        public string CatelogueNo { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public List<int> BatchIds { get; set; }
    }
    public class ProductSampleRequestProductModel
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public string Reason { get; set; }
        public string Handoverby { get; set; }
    }

    public class BrazilModel
    {
        public int Id { get; set; }
        public string InternationalStatus { get; set; }

        [AllowHtml]
        public string Feedback { get; set; }
    }
    }