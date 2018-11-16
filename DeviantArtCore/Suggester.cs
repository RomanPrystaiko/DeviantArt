using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class Suggester
    {
        [DataMember(Name = "userid")]
        public string UserId { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "usericon")]
        public string UserIcon { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
