using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class HDKeyPublicFromXPrvResponse
    {
        /// <summary>
        ///  Public key - 64 symbols hex string
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}