using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
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

        /// <summary>
        /// Flag allowing partial BOC decoding when ABI doesn't describe the full body BOC. Controls decoder behaviour when after decoding all described in ABI params there are some data left in BOC: `true` - return decoded values `false` - return error of incomplete BOC deserialization (default)
        /// </summary>
        [JsonPropertyName("allow_partial")]
        public bool? AllowPartial { get; set; }
    }
}