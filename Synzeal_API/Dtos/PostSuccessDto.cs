using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApiAspNetCore.Dtos
{
    public class PostSuccessDto
    {
        [Required]
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string SAPSONo { get; set; }
        public int SAPSOLId { get; set; }
        public string SAPPONo { get; set; }
        public string SAPSODocEntry { get; set; }

    }
}
