using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SZModel
    {
        public int Id { get; set; }
        public string PONo { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Revised1 { get; set; }
        public string Revised2 { get; set; }
        public string Revised3 { get; set; }
        public Nullable<System.DateTime> ActualDispatch { get; set; }
    }


    public class ConversationReportModel
    {
        public int CountData { get; set; }
        public string companyname { get; set; }
    }
}