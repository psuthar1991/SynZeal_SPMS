using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class TempProductModel
    {
        [AllowHtml]
        public string ProductName { get; set; }
        public string casno { get; set; }
        public string catNo { get; set; }
        [AllowHtml]
        public string price { get; set; }

        [AllowHtml]
        public string finalprice { get; set; }

        [AllowHtml]
        public string leadtime { get; set; }
        [AllowHtml]
        public string productremark { get; set; }
        public int id { get; set; }
        public string estimateDispatchDate { get; set; }
        public bool synthesislog { get; set; }
    }
}