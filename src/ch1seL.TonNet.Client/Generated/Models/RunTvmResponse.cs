using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class RunTvmResponse
    {
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
        /// <para> Updated account state BOC. Encoded as `base64`.</para>
        /// <para> Attention! Only data in account state is updated.</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }
    }
}