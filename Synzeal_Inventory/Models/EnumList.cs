using System.ComponentModel;

namespace Synzeal_Inventory.Models
{
    public class EnumList
    {
        public enum InternationStatusEnum
        {
            //"Open/On hold project/Negotiation/Under Consideration/Closed/Expected PO"
            Open = 1,
            [Description("On hold project")]
            Onholdproject = 2,
            Negotiation = 3,
            [Description("Under Consideration")]
            UnderConsideration = 4,
            Closed = 5,
            [Description("Expected PO")]
            ExpectedPO = 6
        }
        /// <summary>
        /// Enumeration of project type
        /// </summary>
        public enum ProjectType
        {
            Synthesis = 1,
            [Description("Synthesis-Pur")]
            PurSynthesis = 2,
            Purchase = 3,
            [Description("In-Stock")]
            InStock = 4,
            [Description("In-House")]
            InHouse = 5,
            [Description("Drying")]
            Drying = 6,
            [Description("Re-Synthesis")]
            ReSynthesis = 7,
            [Description("Re-Purification")]
            RePurification = 8
        }

        /// <summary>
        /// Enumeration of process state
        /// </summary>
        public enum ProcessState
        {
            InProgress = 0,
            MoveToProject = 1,
            MoveToScientist = 2,
            MoveToDispatch = 3,
            MoveToInvoice = 4,
            MoveToPurchase = 5,
            MoveToInStock = 6
        }

        /// <summary>
        /// Enumeration of scientist status
        /// </summary>
        public enum ProjectStatus
        {
            [Description("No Action")]
            NoAction = 0,
            [Description("Scientist")]
            MoveToScientist = 1,
            [Description("Dispatch")]
            MoveToDispatch = 2,
            [Description("Invoice")]
            MoveToInvoice = 3,
            [Description("Purchase")]
            MoveToPurchase = 4,
            [Description("In-Stock")]
            MoveToInstock = 5,
            [Description("Hold")]
            Hold = 6
        }

        /// <summary>
        /// Enumeration of scientist status
        /// </summary>
        public enum DispatchStatus
        {
            [Description("Unpacked")]
            Unpacked = 0,
            [Description("Packed")]
            Packed = 1,
            [Description("Short Qty")]
            SortQty = 2
        }

        public enum DifficultyLevel
        {
            [Description("A")]
            A = 1,
            [Description("B")]
            B = 2,
            [Description("C")]
            C = 3,
            [Description("D")]
            D = 4,
            [Description("E")]
            E = 5
        }

        public enum PurchaseStatusDDL
        {
            [Description("Temp Hold")]
            TempHold = 15,
            [Description("Regret")]
            RegretRequestSynthesis = 16,
            [Description("RFQ")]
            RFQ = 20,
            [Description("Quotation")]
            Quotation = 21,
            [Description("Quote Approval")]
            QuoteApproval = 22,
            [Description("Quote Approved")]
            QuoteApproved = 23,
            [Description("Ordered")]
            Ordered = 24,
            [Description("Dispatched")]
            Dispatched = 25,
            [Description("Transit")]
            Transit = 26,
            [Description("Delay in Transit")]
            DelayinTransit = 27,
            [Description("Submitted")]
            Submitted = 28,
            [Description("QC Correction")]
            QCCorrection = 33,
            [Description("QC Rejected")]
            QCRejected = 34,

        }

        public enum ProStatusDDL
        {
            [Description("Order Confirmation")]
            OrderConfirmation = 1,
            [Description("Literature & Route")]
            LiteratureandRoute = 2,
            [Description("RM Request")]
            RMRequest = 3,
            [Description("RM Ordered")]
            RMOrdered = 4,
            [Description("RM Delayed")]
            RMDelayed = 5,
            [Description("Regret Request - RM Delayed")]
            RegretRequestRMDelayed = 6,
            [Description("In House RM Synthesis")]
            InHouseRMSynthesis = 7,
            [Description("In Progress")]
            InProgress = 8,
            [Description("Troubleshoot L1")]
            TroubleshootL1 = 9,
            [Description("Troubleshoot L2")]
            TroubleshootL2 = 10,
            [Description("Purification")]
            Purification = 11,
            [Description("Prep HPLC")]
            PrepHPLC = 12,
            [Description("Troubleshoot P1")]
            TroubleshootP1 = 13,
            [Description("Troubleshoot P2")]
            TroubleshootP2 = 14,
            [Description("Temp Hold")]
            TempHold = 15,
            [Description("Regret Request - Synthesis")]
            RegretRequestSynthesis = 16,
            [Description("Regret Request - Purification")]
            RegretRequestPurchase = 17,
            [Description("Under Analysis")]
            UnderAnalysis = 18,
            [Description("Product Confirmation")]
            ProductConfirmation = 19,
            [Description("RFQ")]
            RFQ = 20,
            [Description("Quotation")]
            Quotation = 21,
            [Description("Quote Approval")]
            QuoteApproval = 22,
            [Description("Quote Approved")]
            QuoteApproved = 23,
            [Description("Ordered")]
            Ordered = 24,
            [Description("Dispatched")]
            Dispatched = 25,
            [Description("Transit")]
            Transit = 26,
            [Description("Delay in Transit")]
            DelayinTransit = 27,
            [Description("Submitted")]
            Submitted = 28,
            [Description("Data Pending")]
            DataPending = 29,
            [Description("Re-test Analysis")]
            RetestAnalysis = 30,
            [Description("QC Approval")]
            QCApproval = 31,
            [Description("QC Approved")]
            QCApproved = 32,
            [Description("QC Correction")]
            QCCorrection = 33,
            [Description("QC Rejected")]
            QCRejected = 34,
            [Description("QC Rejected - Drying")]
            QCRejectedDrying = 35,
            [Description("QC Rejected - Re-Synthesis")]
            QCRejectedReSynthesis = 36,
            [Description("QC Rejected - Re-Purification")]
            QCRejectedRePurification = 37,
            [Description("Ready for Dispatch")]
            ReadyforDispatch = 38,
            [Description("Data Sent")]
            DataSent = 39,
            [Description("Data Correction")]
            DataCorrection = 40,
            [Description("Data Approved")]
            DataApproved = 41,
            [Description("Invoice Sent")]
            InvoiceSent = 42,
            [Description("30 Days PDC")]
            ThirtyDaysPDC = 43,
            [Description("Advance Payment")]
            AdvancePayment = 44,
            [Description("Hold - Payment Due")]
            HoldPaymentDue = 45,
            [Description("Hold/Cancelled")]
            HoldAndCancelled = 46,
            [Description("Attention")]
            Attention = 47,
        }

        public enum ProInstockexportStatusDDL
        {
            [Description("Re-test Analysis")]
            RetestAnalysis = 30,
            [Description("Ready for Dispatch")]
            ReadyforDispatch = 38,
            [Description("Data Sent")]
            DataSent = 39,
            [Description("Data Correction")]
            DataCorrection = 40,
            [Description("Data Approved")]
            DataApproved = 41,
            [Description("Invoice Sent")]
            InvoiceSent = 42,
            [Description("30 Days PDC")]
            ThirtyDaysPDC = 43,
            [Description("Advance Payment")]
            AdvancePayment = 44,
            [Description("Hold - Payment Due")]
            HoldPaymentDue = 45,
        }

        //public enum OLDProStatusDDL
        //{
        //    [Description("Literature & Route")]
        //    LiteratureandRoute = 1,
        //    [Description("RM Request")]
        //    RMRequest = 2,
        //    [Description("RM Ordered")]
        //    RMOrdered = 3,
        //    [Description("RM Delayed")]
        //    RMDelayed = 4,
        //    [Description("In Progress")]
        //    InProgress = 5,
        //    [Description("Troubleshoot")]
        //    Troubleshoot =6,
        //    [Description("Attention")]
        //    Attention = 7,
        //    [Description("Temp Hold")]
        //    TempHold = 8,
        //    [Description("Purification")]
        //    Purification = 9,
        //    [Description("Prep HPLC")]
        //    PrepHPLC = 10,
        //    [Description("Under Analysis")]
        //    UnderAnalysis = 11,
        //    [Description("QC Approval")]
        //    QCApproval = 12,
        //    [Description("QC Correction")]
        //    QCCorrection = 13,
        //    [Description("QC Approved")]
        //    QCApproved = 14,
        //    [Description("Ready for Dispatch")]
        //    ReadyforDispatch = 15,
        //    [Description("Data Pending")]
        //    DataPending = 17,
        //    [Description("Client Confirmation")]
        //    ClientConfirmation = 21,
        //    [Description("Regret Request")]
        //    RegretRequest = 23
        //}

        public enum ScientistReviewStatusDDL
        {
            [Description("Literature & Route")]
            LiteratureandRoute = 2,
            [Description("RM Request")]
            RMRequest = 3,
            [Description("RM Ordered")]
            RMOrdered = 4,
            [Description("RM Delayed")]
            RMDelayed = 5,
            [Description("RM Over Delayed")]
            RegretRequestRMDelayed = 6,
            [Description("In House RM Synthesis")]
            InHouseRMSynthesis = 7,
            [Description("In Progress")]
            InProgress = 8,
            [Description("Troubleshoot L1")]
            TroubleshootL1 = 9,
            [Description("Troubleshoot L2")]
            TroubleshootL2 = 10,
            [Description("Purification")]
            Purification = 11,
            [Description("Prep HPLC")]
            PrepHPLC = 12,
            [Description("Troubleshoot P1")]
            TroubleshootP1 = 13,
            [Description("Troubleshoot P2")]
            TroubleshootP2 = 14,
            [Description("Temp Hold")]
            TempHold = 15,
            [Description("Synthesis Challenges")]
            RegretRequestSynthesis = 16,
            [Description("Purification Challenges")]
            RegretRequestPurchase = 17,
            [Description("Under Analysis")]
            UnderAnalysis = 18,
            [Description("Product Confirmation")]
            ProductConfirmation = 19,
            [Description("QC Approval")]
            QCApproval = 31,
            [Description("QC Approved")]
            QCApproved = 32,
            [Description("QC Correction")]
            QCCorrection = 33,
            [Description("QC Rejected")]
            QCRejected = 34,
            [Description("Attention")]
            Attention = 47,
        }

        //public enum OLDScientistReviewStatusDDL
        //{
        //    [Description("Literature & Route")]
        //    LiteratureandRoute = 1,
        //    [Description("RM Request")]
        //    RMRequest = 2,
        //    [Description("RM Ordered")]
        //    RMOrdered = 3,
        //    [Description("RM Delayed")]
        //    RMDelayed = 4,
        //    [Description("In Progress")]
        //    InProgress = 5,
        //    [Description("Troubleshoot")]
        //    Troubleshoot = 6,
        //    [Description("Attention")]
        //    Attention = 7,
        //    [Description("Temp Hold")]
        //    TempHold = 8,
        //    [Description("Purification")]
        //    Purification = 9,
        //    [Description("Prep HPLC")]
        //    PrepHPLC = 10,
        //    [Description("Under Analysis")]
        //    UnderAnalysis = 11,
        //    [Description("QC Approval")]
        //    QCApproval = 12,
        //    [Description("QC Correction")]
        //    QCCorrection = 13,
        //    [Description("QC Approved")]
        //    QCApproved = 14,
        //}

        //public enum OLDProInstockexportStatusDDL
        //{
        //    [Description("QC Approval")]
        //    QCApproval = 12,
        //    [Description("Ready for Dispatch")]
        //    ReadyforDispatch = 15,
        //    [Description("Data Pending")]
        //    DataPending = 17,
        //    [Description("Drying")]
        //    Drying = 18,
        //    [Description("Client Confirmation")]
        //    ClientConfirmation = 21,
        //    [Description("Regret Request")]
        //    RegretRequest = 23,
        //}

        //public enum OLDDispatchStatusDDl
        //{
        //    [Description("QC Correction")]
        //    QCCorrection = 13,
        //    [Description("QC Approved")]
        //    QCApproved = 14,
        //    [Description("Ready for Dispatch")]
        //    ReadyforDispatch = 15,
        //    [Description("Data Pending")]
        //    DataPending = 17,
        //    [Description("QC Approval")]
        //    QCApproval = 12,
        //}

        public enum DispatchStatusDDl
        {
            [Description("Order Confirmation")]
            OrderConfirmation = 1,
            [Description("Data Pending")]
            DataPending = 29,
            [Description("Re-test Analysis")]
            RetestAnalysis = 30,
            [Description("QC Approval")]
            QCApproval = 31,
            [Description("QC Approved")]
            QCApproved = 32,
            [Description("QC Correction")]
            QCCorrection = 33,
            [Description("QC Rejected")]
            QCRejected = 34,
            [Description("QC Rejected - Drying")]
            QCRejectedDrying = 35,
            [Description("QC Rejected - Re-Synthesis")]
            QCRejectedReSynthesis = 36,
            [Description("QC Rejected - Re-Purification")]
            QCRejectedRePurification = 37,
            [Description("Ready for Dispatch")]
            ReadyforDispatch = 38,
            [Description("Data Sent")]
            DataSent = 39,
            [Description("Data Correction")]
            DataCorrection = 40,
            [Description("Data Approved")]
            DataApproved = 41,
            [Description("Invoice Sent")]
            InvoiceSent = 42,
            [Description("30 Days PDC")]
            ThirtyDaysPDC = 43,
            [Description("Advance Payment")]
            AdvancePayment = 44,
            [Description("Hold - Payment Due")]
            HoldPaymentDue = 45
        }

        public enum ApprovedStatus
        {
            [Description("QC Approval")]
            QCApproval = 31,
            [Description("QC Approved")]
            QCApproved = 32,
            [Description("QC Correction")]
            QCCorrection = 33,
            [Description("QC Rejected")]
            QCRejected = 34,
            [Description("QC Rejected - Drying")]
            QCRejectedDrying = 35,
            [Description("QC Rejected - Re-Synthesis")]
            QCRejectedReSynthesis = 36,
            [Description("QC Rejected - Re-Purification")]
            QCRejectedRePurification = 37,
            [Description("Data Correction")]
            DataCorrection = 40
        }
        //public enum OLDApprovedStatus
        //{
        //    [Description("QC Approval")]
        //    QCApproval = 0,
        //    [Description("QC Approved")]
        //    QCApproved = 1,
        //    [Description("QC Correction")]
        //    QCCorrection = 2,
        //}
        public enum ApprovedAs
        {
            [Description("Qualified Working Standard")]
            QualifiedWorkingStandard = 1,
            [Description("SynStandard")]
            SynStandard = 2,
            [Description("Qualitative Standard")]
            QualitativeStandard = 3,
            [Description("Quantitative Standard")]
            QuantitativeStandard = 4,
            [Description("Current Dispatch Approved")]
            CurrentDispatchApproved = 5,
            [Description("Traceable Working Standard")]
            TraceableWorkingStandard = 6,
        }

        public enum PRRequestStatus
        {
            [Description("PR Created")]
            PRCreated = 1,
            [Description("RFQ Floated")]
            RFQFloated = 2,
            [Description("Quotation Received")]
            QuotationReceived = 3,
            [Description("Approval Pending")]
            ApprovalPending = 4,
            [Description("PO Released")]
            POReleased = 5,
            [Description("In Transit")]
            InTransit = 6,
            [Description("Received")]
            Received = 7

        }

        public enum GeographicalLocationOLD
        {
            [Description("Latin America")]
            LatinAmerica = 1,
            [Description("China, Korea, Japan, Russia")]
            ChinaKoreaJapanRussia = 2,
            [Description("Europe")]
            Europe = 3,
            [Description("US, Canada, North America")]
            USCanadaNorthAmerica = 4,
            [Description("Domestic")]
            Domestic = 5,
            [Description("Middle East & Gulf")]
            MiddleEastGulf = 6,
            [Description("Africa")]
            Africa = 7,
            [Description("Australia, New-Zealand, Indonesia")]
            AustraliaNewZealandIndonesia = 8,
            [Description("South Asia")]
            SouthAsia = 9
        }
    }
}