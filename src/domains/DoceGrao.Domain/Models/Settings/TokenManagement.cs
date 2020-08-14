using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DoceGrao.Api.Domain.Models.Settings
{
    public class TokenManagement
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
        [JsonPropertyName("audience")]
        public string Audience { get; set; }
        [JsonPropertyName("accessExpiration")]
        public int AccessExpiration { get; set; }
        [JsonPropertyName("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
