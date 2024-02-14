using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_BatchFormData_Sync.Model
{
    public class SZ_QuoteDetails_FormModel
    {
        public int Id { get; set; }
        public int QuoteDetailsId { get; set; }
        public int FormId { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
