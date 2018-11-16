using System;
using System.Runtime.Serialization;

namespace DeviantArtCore
{
    [DataContract]
    public class Author
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
