using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class UserDashboardDto
    {
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Username { get; set; }
        public int? AllDetails { get; set; }
        public int? WithoutCatNo { get; set; }
        public int? WithoutCasNo { get; set; }
        public int? WithoutPrice { get; set; }
        public int? TotalAdded { get; set; }
        public int? RevisionCatNo { get; set; }
        public int? RevisionCasNo { get; set; }
        public int? RevisionPrice { get; set; }
        public int? TotalRevisionProduct { get; set; }
    }

    public class MonthWiseUserDashboardDto
    {
        public string MonthName { get; set; }
        public int Year { get; set; }

        public IEnumerable<UserDashboardDto> UserDashboardModel { get; set; }
    }


    public class UserDashboardSummaryDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int? InStock { get; set; }
        public int? Synthesis { get; set; }
        public int? Reviewed { get; set; }
        public int? Approved { get; set; }
        public int? Deleted { get; set; }
        public int? Park { get; set; }
        public int? Clarification { get; set; }
        public int? Upload { get; set; }
        public int? Complex { get; set; }
        public int? Regretted { get; set; }
        public int? Total { get; set; }
    }

    public class ExportPriceModel
    {
        public string CATno { get; set; }
        public string CASNo { get; set; }

        public List<ExportPriceDetailModel> PriceData { get; set; }
    }

    public class ExportPriceDetailModel
    {
        public string Mg { get; set; }
        public string Price { get; set; }

        public string Currency { get; set; }
    }
}