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
    public class ResultOfEncodeMessage
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Returned in case of `Signer::External`. Can be used for external</para>
        /// <para>message signing. Is this case you need to use this data to create signature and</para>
        /// <para>then produce signed message using `abi.attach_signature`.</para>
        /// </summary>
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}