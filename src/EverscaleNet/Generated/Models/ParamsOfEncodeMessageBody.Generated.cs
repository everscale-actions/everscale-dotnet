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
    public class ParamsOfEncodeMessageBody
    {
        /// <summary>
        /// Contract ABI.
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Function call parameters.</para>
        /// <para>Must be specified in non deploy message.</para>
        /// <para>In case of deploy message contains parameters of constructor.</para>
        /// </summary>
        [JsonPropertyName("call_set")]
        public CallSet CallSet { get; set; }

        /// <summary>
        /// True if internal message body must be encoded.
        /// </summary>
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }

        /// <summary>
        /// Signing parameters.
        /// </summary>
        [JsonPropertyName("signer")]
        public Signer Signer { get; set; }

        /// <summary>
        /// <para>Processing try index.</para>
        /// <para>Used in message processing with retries.</para>
        /// <para>Encoder uses the provided try index to calculate message</para>
        /// <para>expiration time.</para>
        /// <para>Expiration timeouts will grow with every retry.</para>
        /// <para>Default value is 0.</para>
        /// </summary>
        [JsonPropertyName("processing_try_index")]
        public byte? ProcessingTryIndex { get; set; }
    }
}