using System;
using System.Collections.Generic;
using System.Text;

namespace deviantArt.Shared.Models
{
    public class ImageItem
    {
        public string Src { get; set; }

        public bool IsDownloadable { get; set; }

        public string ContentSource { get; set; }

        public string DeviationId { get; set; }
    }
}
