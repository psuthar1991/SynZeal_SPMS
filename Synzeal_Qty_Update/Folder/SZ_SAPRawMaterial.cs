//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Qty_Update.Folder
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_SAPRawMaterial
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public string Requester { get; set; }
        public string Req_UserName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CASNo { get; set; }
        public string CatlogNum { get; set; }
        public decimal PackSize { get; set; }
        public string Make { get; set; }
        public System.DateTime PR_Date { get; set; }
        public int PR_Number { get; set; }
        public System.DateTime PO_Date { get; set; }
        public int PO_Number { get; set; }
        public decimal PO_Qty { get; set; }
        public decimal PO_Remain_Qty { get; set; }
        public int Days_Req_2_TillDate { get; set; }
        public string Project { get; set; }
    }
}
