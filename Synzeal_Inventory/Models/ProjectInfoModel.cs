using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class ProjectInfoModel
    {
        public int? scId { get; set; }
        public string subScientistId { get; set; }
        public string projectType { get; set; }
        public string qty { get; set; }
        public int id { get; set; }
        public string batchno { get; set; }
        [AllowHtml]
        public string remark { get; set; }
        [AllowHtml]
        public string additionalBatch { get; set; }
        [AllowHtml]
        public string scientistStatus { get; set; }
        [AllowHtml]
        public string sciremark { get; set; }
        [AllowHtml]
        public string purchaseStatus{ get; set; }
        public string tabname{ get; set; }
        public string estDate { get; set; }
        [AllowHtml]
        public string orderremark { get; set; }
        [AllowHtml]
        public string reason { get; set; }
        public string reportinvoiceDate { get; set; }
        public string difflevel { get; set; }
        [AllowHtml]
        public string explainationsecond { get; set; }
        [AllowHtml]
        public string activity { get; set; }
        [AllowHtml]
        public string purchaseremark { get; set; }
        [AllowHtml]
        public string purchaseDDLStatus { get; set; }
        [AllowHtml]
        public string prostatus { get; set; }

        [AllowHtml]
        public string otherprostatus { get; set; }

        public string CoaId { get; set; }
        public bool IsGLP { get; set; }
        public int GLPStatus { get; set; }
        public string analysisCompletionDate { get; set; }
    }

    public class ControlledSubstanceModel
    {
        public int id { get; set; }
        public string tabname { get; set; }
        public int LicesenStatus { get; set; }
        public string PermitRequired { get; set; }
        public string ImportPermit { get; set; }
        public string Declaration { get; set; }
        public DateTime? ExportPermitReceivedDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string TechnicalWriteup { get; set; }
        public string QuaterlyDataToSubmit { get; set; }
        public string QuaterlyDataSubmited { get; set; }
        public string JourneyComment { get; set; }
        public string APIRequired { get; set; }
        public string APIImpExport { get; set; }
        public string Category { get; set; }
        public string NextQuater { get; set; }
        public string ImpExpo { get; set; }
    }
        public class QCInfoModel
    {
        public int QuotationDetailsId { get; set; }
        public int formId { get; set; }
        [AllowHtml]
        public string ApprovedStatus { get; set; }
        [AllowHtml]
        public string ApprovedAs { get; set; }
        [AllowHtml]
        public string ApprovalComments { get; set; }
        [AllowHtml]
        public string RecommondationRetest { get; set; }

        [AllowHtml]
        public string PrimaryStdOrdered { get; set; }
        [AllowHtml]
        public string ColumnOrder { get; set; }
        [AllowHtml]
        public string SystemSuitability { get; set; }

        public string TableId { get; set; }
        public int? BatchId { get; set; }
    }
}