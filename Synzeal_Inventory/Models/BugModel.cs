using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class BugModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> ResolveDate { get; set; }
        public string BugNo { get; set; }
        public string Subject { get; set; }
    }


    public class StructureSearchPostModel
    {
        public string query { get; set; }
        public string searchType { get; set; }
        public string target { get; set; }
    }
}