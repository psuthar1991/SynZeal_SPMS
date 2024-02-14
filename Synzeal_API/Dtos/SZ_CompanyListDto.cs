using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Dtos
{
    public class SZ_CompanyListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MasterEmail { get; set; }
        public string CountryType { get; set; }
        public Nullable<int> TermsId { get; set; }
        public string UserDistType { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
    }
}
