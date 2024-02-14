using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ReactionMatrixModel
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public int QuoteDetailId { get; set; }
        public int? ProductId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime SchemeFromDate { get; set; }
        public System.DateTime SchemeEndDate { get; set; }
        public string Scheme { get; set; }
        public int TotalStep { get; set; }
        public int StepNo { get; set; }
        public int NoofReaction { get; set; }
        public int SuccessReaction { get; set; }
        public int NoofPurification { get; set; }
        public string BatchSize { get; set; }
        public string ExpNo { get; set; }
        public int SrNo { get; set; }
        public string Range { get; set; }
    }
}