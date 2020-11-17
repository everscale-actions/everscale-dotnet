using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class RunExecutorResponse
    {
        /// <summary>
        /// <para> Parsed transaction.</para>
        /// <para> In addition to the regular transaction fields there is a</para>
        /// <para> `boc` field encoded with `base64` which contains source</para>
        /// <para> transaction BOC.</para>
        /// </summary>
        [JsonPropertyName("transaction")]
        public JsonElement Transaction { get; set; }

        /// <summary>
        ///  List of output messages' BOCs. Encoded as `base64`
        /// </summary>
        [JsonPropertyName("out_messages")]
        public string[] OutMessages { get; set; }

        /// <summary>
        /// <para> Optional decoded message bodies according to the optional</para>
        /// <para> `abi` parameter.</para>
        /// </summary>
        [JsonPropertyName("decoded")]
        public DecodedOutput Decoded { get; set; }

        /// <summary>
        ///  Updated account state BOC. Encoded as `base64`
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        ///  Transaction fees
        /// </summary>
        [JsonPropertyName("fees")]
        public TransactionFees Fees { get; set; }
    }
}