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
    public class ResultOfEncodeMessageBody
    {
        /// <summary>
        /// Message body BOC encoded with `base64`.
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// <para>Optional data to sign.</para>
        /// <para>Encoded with `base64`. </para>
        /// <para>Presents when `message` is unsigned. Can be used for external</para>
        /// <para>message signing. Is this case you need to sing this data and</para>
        /// <para>produce signed message using `abi.attach_signature`.</para>
        /// </summary>
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }
    }
}