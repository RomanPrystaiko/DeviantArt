using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class DeviantItem
    {
        [DataMember(Name = "deviationid")]
        public string DeviationId { get; set; }

        [DataMember(Name = "printid")]
        public string PrintId { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "category_path")]
        public string CategoryPath { get; set; }

        [DataMember(Name = "is_favourited")]
        public bool IsFavourited { get; set; }

        [DataMember(Name = "is_deleted")]
        public bool IsDeleted { get; set; }

        [DataMember(Name = "author")]
        public Author Author { get; set; }

        [DataMember(Name = "stats")]
        public Stats Stats { get; set; }

        [DataMember(Name = "published_time")]
        public string PublishedTime { get; set; }

        [DataMember(Name = "allows_comments")]
        public bool AllowsComments { get; set; }

        [DataMember(Name = "preview")]
        public Preview Preview { get; set; }

        [DataMember(Name = "content")]
        public Content Content { get; set; }

        [DataMember(Name = "thumbs")]
        public List<Thumb> Thumbs { get; set; }

        [DataMember(Name = "is_mature")]
        public bool IsMature { get; set; }

        [DataMember(Name = "is_downloadable")]
        public bool IsDownloadable { get; set; }

        [DataMember(Name = "download_filesize")]
        public int DownloadFilesize { get; set; }

        [DataMember(Name = "flash")]
        public Flash Flash { get; set; }

        [DataMember(Name = "excerpt")]
        public string Excerpt { get; set; }

        [DataMember(Name = "daily_deviation")]
        public DailyDeviation DailyDeviation { get; set; }
    }
}
