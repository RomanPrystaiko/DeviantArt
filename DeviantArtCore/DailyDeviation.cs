using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class DailyDeviation
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "time ")]
        public DateTime Time { get; set; }

        [DataMember(Name = "giver")]
        public Giver Giver { get; set; }

        [DataMember(Name = "suggester")]
        public Suggester Suggester { get; set; }
    }
}
