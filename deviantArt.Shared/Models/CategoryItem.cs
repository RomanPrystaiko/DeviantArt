using deviantArt.Shared.DAL;
using DeviantArtCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace deviantArt.Shared.Models
{
    public class CategoryItem
    {
        public string ItemName { get; set; }
        public string IconText { get; set; }
        public ImageCatogory ImageCategory { get; set; }
    }
}
