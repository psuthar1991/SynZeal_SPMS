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
    
    public partial class SZ_QuoteDetailForm
    {
        public int Id { get; set; }
        public int QuotationDetailsId { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public string CATNo { get; set; }
        public string ProjectName { get; set; }
        public string CASNo { get; set; }
        public string ProductName { get; set; }
        public string ScientistName { get; set; }
        public string BatchCode { get; set; }
        public System.DateTime JournalDate { get; set; }
        public string MolWeight { get; set; }
        public string MolFormula { get; set; }
        public string NMRCode { get; set; }
        public string HPCLCode { get; set; }
        public string MSCode { get; set; }
        public string OtherAnalysis { get; set; }
        public string TypeCompound { get; set; }
        public string StateCompound { get; set; }
        public string SaltName { get; set; }
        public string StructurePath { get; set; }
        public string Qty { get; set; }
        public string Apearance { get; set; }
        public string Error { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string RbSaltMentionName { get; set; }
        public int SubmittedBy { get; set; }
        public string TLName { get; set; }
        public string HPLCDate { get; set; }
        public string HPLCPurity { get; set; }
        public Nullable<bool> ChkHygroscopic { get; set; }
        public string RbAdditionalAnalysis { get; set; }
        public string MolecularFormula { get; set; }
        public string SolutionForm { get; set; }
        public string SolidForm { get; set; }
        public string State { get; set; }
        public Nullable<int> NoOfFinalStep { get; set; }
        public string EarlierSynthesized { get; set; }
        public string PurificationBy { get; set; }
        public string SpectralDataAttachment { get; set; }
        public string Chemist { get; set; }
        public Nullable<bool> TempSensitive { get; set; }
        public Nullable<bool> Lacrymatory { get; set; }
        public string StabilityRelatedComment { get; set; }
        public string Stability { get; set; }
        public string IRAttachment { get; set; }
        public string MassAttachment { get; set; }
        public string PLCAttachment { get; set; }
        public string NMRAttchment { get; set; }
        public string QNMRAttchment { get; set; }
        public string TGAAttachment { get; set; }
        public string CMRAttchment { get; set; }
        public string DEPTAttachment { get; set; }
        public string HRMSAttachment { get; set; }
        public string ROIAttachment { get; set; }
        public string ElementralAttachment { get; set; }
        public string SERAttachment { get; set; }
        public string GCAttachment { get; set; }
        public string ELSDAttachment { get; set; }
        public string ChiralAttachmenrt { get; set; }
        public Nullable<bool> IsDraftEntry { get; set; }
        public Nullable<bool> chkNMRDone { get; set; }
        public Nullable<bool> chkCrystallizationDone { get; set; }
        public string APCIMassAttachment { get; set; }
        public string ChemdrawFileAttachment { get; set; }
        public string WeightingSlipAttachment { get; set; }
        public string NMRInterpretaionAttachment { get; set; }
        public Nullable<bool> IsDispatchedEntry { get; set; }
        public Nullable<bool> LightSensitivity { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedAs { get; set; }
        public string ApprovalComment { get; set; }
        public string RecommendedPeriod { get; set; }
        public Nullable<System.DateTime> QCApprovedDate { get; set; }
        public Nullable<bool> Photostability { get; set; }
        public string UVSpectra { get; set; }
        public string OtherAnalysisAttachment { get; set; }
        public string N1NmrAttachment { get; set; }
        public string ChiralHPLCAttachment { get; set; }
        public string IsotropicpurityAttachment { get; set; }
        public string TwoDNMRAttachment { get; set; }
        public string COSYAttachment { get; set; }
        public string CHNSAttachment { get; set; }
        public string StabilitydataAttachment { get; set; }
        public Nullable<bool> IsLight { get; set; }
        public string ELNReceiptNumber { get; set; }
        public string LCMSAttachment { get; set; }
        public string Ref { get; set; }
        public string Source { get; set; }
        public string Reviewer { get; set; }
        public string Approver { get; set; }
        public Nullable<bool> Isboughtout { get; set; }
    
        public virtual SZ_QuotationDetail SZ_QuotationDetail { get; set; }
    }
}