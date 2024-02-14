using System.ComponentModel;

namespace SampleWebApiAspNetCore.Models
{
    /// <summary>
    /// Represents the product sorting
    /// </summary>
    public enum ProductSortingEnum
    {
        /// <summary>
        /// Position (display order)
        /// </summary>
        Position = 0,
        /// <summary>
        /// Name: A to Z
        /// </summary>
        NameAsc = 5,
        /// <summary>
        /// Name: Z to A
        /// </summary>
        NameDesc = 6,
        /// <summary>
        /// Price: Low to High
        /// </summary>
        PriceAsc = 10,
        /// <summary>
        /// Price: High to Low
        /// </summary>
        PriceDesc = 11,
        /// <summary>
        /// Product creation date
        /// </summary>
        CreatedOn = 15,
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
}