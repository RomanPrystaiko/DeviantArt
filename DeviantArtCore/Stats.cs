using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class Stats
    {
        [DataMember(Name = "comments")]
        public int Comments { get; set; }

        [DataMember(Name = "favourites")]
        public int Favourites { get; set; }
    }
}
