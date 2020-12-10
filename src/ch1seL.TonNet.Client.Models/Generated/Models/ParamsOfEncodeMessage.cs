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
    public class ParamsOfEncodeMessage
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Must be specified in case of non-deploy message.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Must be specified in case of deploy message.
        /// </summary>
        [JsonPropertyName("deploy_set")]
        public DeploySet DeploySet { get; set; }

        /// <summary>
        /// <para>Must be specified in case of non-deploy message.</para>
        /// <para>In case of deploy message it is optional and contains parameters</para>
        /// <para>of the functions that will to be called upon deploy transaction.</para>
        /// </summary>
        [JsonPropertyName("call_set")]
        public CallSet CallSet { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("signer")]
        public Signer Signer { get; set; }

        /// <summary>
        /// <para>Used in message processing with retries (if contract's ABI includes "expire" header).</para>
        /// <para>Encoder uses the provided try index to calculate message</para>
        /// <para>expiration time. The 1st message expiration time is specified in</para>
        /// <para>Client config.</para>
        /// <para>Expiration timeouts will grow with every retry.</para>
        /// <para>Retry grow factor is set in Client config:</para>
        /// <para>&lt;.....add config parameter with default value here&gt;</para>
        /// <para>Default value is 0.</para>
        /// </summary>
        [JsonPropertyName("processing_try_index")]
        public byte? ProcessingTryIndex { get; set; }
    }
}