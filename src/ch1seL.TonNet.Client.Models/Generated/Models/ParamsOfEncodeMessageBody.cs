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
    public class ParamsOfEncodeMessageBody
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Must be specified in non deploy message.</para>
        /// <para>In case of deploy message contains parameters of constructor.</para>
        /// </summary>
        [JsonPropertyName("call_set")]
        public CallSet CallSet { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("signer")]
        public Signer Signer { get; set; }

        /// <summary>
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