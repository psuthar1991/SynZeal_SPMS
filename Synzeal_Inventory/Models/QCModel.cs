using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class QCModel
    {
        public bool? IsConversionReport { get; set; }
        public bool IsLatinAmerica { get; set; }
        public int? ScientistCustomerId { get; set; }
        public string CompanyName { get; set; }
        public int FormId { get; set; }
        public int QuoteDetailsId { get; set; }
        public string QuoteRefNo { get; set; }
        public string ScientistName { get; set; }
        public string Chemist { get; set; }
        public string ProductName { get; set; }
        public string CASNo { get; set; }
        public string Diff { get; set; }
        public string CATNo { get; set; }
        public string Qty { get; set; }
        public string SubmittedQty { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string ReviewStatus { get; set; }
        public DateTime? AssignDate { get; set; }
        public string SubmissionDatestr { get; set; }
        public string AssignDatestr { get; set; }
        public int? AdditionalBatchNo { get; set; }
        public int? ProductId { get; set; }
        
        public string SelectedAdditionalBatchNo { get; set; }
        public string AdditionalBatchNoText { get; set; }
        public string BatchNo { get; set; }
        public string DataRemark { get; set; }
        public string DraftSubmissionForm { get; set; }
        public string AvailableData { get; set; }
        public string PhysicalState { get; set; }
        public string Appearance { get; set; }
        public bool? Hygroscopic { get; set; }
        public string TextHygroscopic { get; set; }
        public bool? TempSensitive { get; set; }
        public bool? Lacrymatory { get; set; }
        public bool? LightSensitivity { get; set; }
        public string TextTempSensitive { get; set; }
        public string TextLacrymatory { get; set; }

        public string TextPhotostability { get; set; }
        
        public string TextLightSensitivity { get; set; }
        public string StabilityRelatedComment { get; set; }
        public string Stability { get; set; }
        public string SaltFreeBase { get; set; }
        public string COA { get; set; }
        public string HPLC { get; set; }
        public string RbAdditionalAnalysis { get; set; }
        public string IR { get; set; }
        public string Mass { get; set; }
        public string HPLCGCELSD { get; set; }
        public string NMR { get; set; }
        public string qNMR { get; set; }
        public string TGA { get; set; }
        public string CMR { get; set; }
        public string DEPT { get; set; }
        public string HRMS { get; set; }
        public string ROI { get; set; }
        public string Elemental { get; set; }
        public string SER { get; set; }
        public string GC { get; set; }
        public string ELSD { get; set; }
        public string Chairal { get; set; }

        public string APCIMass { get; set; }
        public string NMRInterpretaion { get; set; }
        public string ChemDrawFile { get; set; }
        public string IRAttachment { get; set; }
        public string MassAttachment { get; set; }
        public string HPLCGCELSDAttachment { get; set; }
        public string NMRAttachment { get; set; }
        public string qNMRAttachment { get; set; }
        public string TGAAttachment { get; set; }
        public string CMRAttachment { get; set; }
        public string DEPTAttachment { get; set; }
        public string HRMSAttachment { get; set; }
        public string ROIAttachment { get; set; }
        public string ElementalAttachment { get; set; }
        public string SERAttachment { get; set; }
        public string GCAttachment { get; set; }
        public string ELSDAttachment { get; set; }
        public string ChairalAttachment { get; set; }

        public string APCIMassAttachment { get; set; }
        public string NMRInterpretaionAttachment { get; set; }

        public string ChemdrawFileAttachment { get; set; }
        public string N1NMRAttachment { get; set; }

        public string ChiralHPLCAttachment { get; set; }
        public string IsotropicpurityAttachment { get; set; }
        public string TwoDNMRAttachment { get; set; }

        public string ProStatus { get; set; }
        public string ApprovedStatus { get; set; }
        public string ApprovedStatusText { get; set; }
        public string ApprovedAsText { get; set; }
        public string ApprovedAs { get; set; }
        public string ApprovalCommentsText { get; set; }
        public string ApprovalComments { get; set; }
        public string RecommondationRetest { get; set; }
        public string RecommondationRetestText { get; set; }
        public string APIName { get; set; }
        public string ChkSaveRow { get; set; }
        public string PrimaryStdOrdered { get; set; }
        public string ColumnOrder { get; set; }
        public string SystemSuitability { get; set; }
        public string PrimaryStdOrderedText { get; set; }
        public string ColumnOrderText { get; set; }
        public string SystemSuitabilityText { get; set; }

        public string StabilitySolution { get; set; }
        public string StabilityRT { get; set; }
        public string NMRLaststep { get; set; }
        public string CrystallizationSteps { get; set; }

        public Nullable<bool> chkNMRDone { get; set; }
        public Nullable<bool> chkCrystallizationDone { get; set; }

        public string chkNMRDoneText { get; set; }
        public string chkCrystallizationDoneText { get; set; }

        public bool? Photostability { get; set; }
        public string SpecialStorageCondition { get; set; }

        public DateTime? QCApprovedDate { get; set; }
        public string QCApprovedDateStr { get; set; }

        public string GetAllBatch { get; set; }

        public string COSYAttachment { get; set; }
        public string CHNSAttachment { get; set; }
        public string StabilitydataAttachment { get; set; }

        public string TwoDNmr { get; set; }
        public string COSY { get; set; }
        public string CHNS { get; set; }
        public string Stabilitydata { get; set; }
        public Nullable<System.DateTime> QcApprovalDate { get; set; }
        public string QcApprovalStr { get; set; }

        public string QcLastApprovalStr { get; set; }

        public SZ_QuotationDetail QuoteDetails { get; set; }
}

    public class QuoteNotificationModel
    {
        public string FieldName { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public System.DateTime DDateTime { get; set; }
        public string DateTime { get; set; }
        public string Username { get; set; }
        public string QuoteLink { get; set; }
        public string Ref { get; set; }

        public int QuoteId { get; set; }
    }
}