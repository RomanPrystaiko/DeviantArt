using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace DeviantArtCore.Authentification
{
    [DataContract]
    public class PublicAccessToken
    {
        [DataMember(Name ="access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
