using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synzeal_API.Dtos
{
    public class PictureModelDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string FullSizeImageUrlWithWaterMark { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }
}
