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
    public class ParamsOfDecodeMessageBody
    {
        /// <summary>
        /// Contract ABI used to decode.
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Message body BOC encoded in `base64`.
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// True if the body belongs to the internal message.
        /// </summary>
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }
    }
}