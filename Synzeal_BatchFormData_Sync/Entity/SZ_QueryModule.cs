//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_BatchFormData_Sync.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_QueryModule
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string QueryType { get; set; }
        public Nullable<int> QuoteDetailsId { get; set; }
        public string Origin { get; set; }
        public string SubStatus { get; set; }
        public string Status { get; set; }
        public string ProblemType { get; set; }
        public string ProblemSubType { get; set; }
        public Nullable<int> TeamLeaderId { get; set; }
        public string TeamLeader { get; set; }
        public Nullable<int> ScientistId { get; set; }
        public string Scientist { get; set; }
        public string Priority { get; set; }
        public Nullable<int> ClosingOn { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string ClientRemark { get; set; }
        public string PrimaryEmail { get; set; }
        public string CCEmail { get; set; }
        public string Attachment { get; set; }
        public Nullable<System.DateTime> PoDate { get; set; }
        public string PONo { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string CATNo { get; set; }
        public string BatchNo { get; set; }
        public string Qty { get; set; }
        public string SynzealRemark { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string SolutionType { get; set; }
        public Nullable<int> SolutionId { get; set; }
        public string SolutionText { get; set; }
        public Nullable<bool> IsScientistResolved { get; set; }
        public Nullable<bool> IsDispatched { get; set; }
        public Nullable<bool> IsAnalytical { get; set; }
        public string QueryNo { get; set; }
        public string QuerySubject { get; set; }
        public string CreatedUserName { get; set; }
        public string SubScientistName { get; set; }
        public Nullable<System.DateTime> InProcessDate { get; set; }
        public Nullable<System.DateTime> SolvedDate { get; set; }
    }
}
