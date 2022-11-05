using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfDecodeMessageBody
    {
        /// <summary>
        /// <para>Contract ABI used to decode.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Message body BOC encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// <para>True if the body belongs to the internal message.</para>
        /// </summary>
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }

        /// <summary>
        /// <para>Flag allowing partial BOC decoding when ABI doesn't describe the full body BOC. Controls decoder behaviour when after decoding all described in ABI params there are some data left in BOC: `true` - return decoded values `false` - return error of incomplete BOC deserialization (default)</para>
        /// </summary>
        [JsonPropertyName("allow_partial")]
        public bool? AllowPartial { get; set; }
    }
}