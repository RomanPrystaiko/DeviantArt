using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class DeviantItemCollection
    {
        private bool disposed = false;

        [DataMember(Name = "has_more")]
        public bool HasMore { get; set; }

        [DataMember(Name = "next_offset")]
        public int NextOffset { get; set; }

        [DataMember(Name = "results")]
        public List<DeviantItem> DeviantItems { get; set; }
    }
}
