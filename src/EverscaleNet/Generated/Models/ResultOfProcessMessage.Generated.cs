using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfProcessMessage
    {
        /// <summary>
        /// <para>Parsed transaction.</para>
        /// <para>In addition to the regular transaction fields there is a</para>
        /// <para>`boc` field encoded with `base64` which contains source</para>
        /// <para>transaction BOC.</para>
        /// </summary>
        [JsonPropertyName("transaction")]
        public JsonElement? Transaction { get; set; }

        /// <summary>
        /// <para>List of output messages' BOCs.</para>
        /// <para>Encoded as `base64`</para>
        /// </summary>
        [JsonPropertyName("out_messages")]
        public string[] OutMessages { get; set; }

        /// <summary>
        /// <para>Optional decoded message bodies according to the optional `abi` parameter.</para>
        /// </summary>
        [JsonPropertyName("decoded")]
        public DecodedOutput Decoded { get; set; }

        /// <summary>
        /// <para>Transaction fees</para>
        /// </summary>
        [JsonPropertyName("fees")]
        public TransactionFees Fees { get; set; }
    }
}