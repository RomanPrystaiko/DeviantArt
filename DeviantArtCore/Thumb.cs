using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class Thumb
    {
        [DataMember(Name = "src")]
        public string Src { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "transparency")]
        public bool Transparency { get; set; }
    }
}
