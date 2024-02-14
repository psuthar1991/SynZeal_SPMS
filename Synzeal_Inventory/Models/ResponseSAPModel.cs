using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class MEXTResponseDetail
    {
        public string DocObject { get; set; }
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string Remark { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<MEXTResponseDetailsId> MEXT_Response_Details_Id { get; set; }
    }

    public class MEXTResponseDetailsId
    {
        public string ExtPK_LId { get; set; }
        public string LineNum { get; set; }
    }

    public class ResponseSAPModel
    {
        public string ExtPK { get; set; }
        public string Remark { get; set; }
        public string TrnType { get; set; }
        public DateTime Creation_Date { get; set; }
        public List<MEXTResponseDetail> MEXT_Response_Details { get; set; }
    }
}