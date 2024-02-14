using System;
using System.Linq;

namespace SampleWebApiAspNetCore.Models
{
    public class QueryParameters
    {
        private const int maxPageCount = 50;
        public int Page { get; set; } = 1;

        private int _pageCount = maxPageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
        }

        public string Query { get; set; }

        public string OrderBy { get; set; } = "Name";
    }


    public class PreviousInfoParameter
    {
        public string ProductName { get; set; }
        public string casno { get; set; }
        public string catNo { get; set; }
        public string company { get; set; }
        public int QuoteId { get; set; } = 0;
        public bool isApi { get; set; } = false;
        }
}