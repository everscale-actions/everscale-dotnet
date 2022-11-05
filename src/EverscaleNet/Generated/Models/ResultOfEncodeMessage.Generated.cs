using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncodeMessage
    {
        /// <summary>
        /// <para>Message BOC encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Optional data to be signed encoded in `base64`.</para>
        /// <para>Returned in case of `Signer::External`. Can be used for external</para>
        /// <para>message signing. Is this case you need to use this data to create signature and</para>
        /// <para>then produce signed message using `abi.attach_signature`.</para>
        /// </summary>
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }

        /// <summary>
        /// <para>Destination address.</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// <para>Message id.</para>
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}