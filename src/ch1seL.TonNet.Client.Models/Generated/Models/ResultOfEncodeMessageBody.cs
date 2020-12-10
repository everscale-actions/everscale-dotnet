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
    public class ResultOfEncodeMessageBody
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// <para>Encoded with `base64`.</para>
        /// <para>Presents when `message` is unsigned. Can be used for external</para>
        /// <para>message signing. Is this case you need to sing this data and</para>
        /// <para>produce signed message using `abi.attach_signature`.</para>
        /// </summary>
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }
    }
}