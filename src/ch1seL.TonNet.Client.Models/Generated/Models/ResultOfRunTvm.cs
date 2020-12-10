using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ResultOfRunTvm
    {
        /// <summary>
        /// Encoded as `base64`
        /// </summary>
        [JsonPropertyName("out_messages")]
        public string[] OutMessages { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("decoded")]
        public DecodedOutput Decoded { get; set; }

        /// <summary>
        /// Encoded as `base64`.Attention! Only data in account state is updated.
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }
    }
}