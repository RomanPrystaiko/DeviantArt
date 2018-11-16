using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviantArtCore
{
    [DataContract]
    public class ApiStatus
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
