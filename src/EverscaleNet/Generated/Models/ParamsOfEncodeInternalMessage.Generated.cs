using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeInternalMessage
    {
        /// <summary>
        /// <para>Contract ABI.</para>
        /// <para>Can be None if both deploy_set and call_set are None.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Target address the message will be sent to.</para>
        /// <para>Must be specified in case of non-deploy message.</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// <para>Source address of the message.</para>
        /// </summary>
        [JsonPropertyName("src_address")]
        public string SrcAddress { get; set; }

        /// <summary>
        /// <para>Deploy parameters.</para>
        /// <para>Must be specified in case of deploy message.</para>
        /// </summary>
        [JsonPropertyName("deploy_set")]
        public DeploySet DeploySet { get; set; }

        /// <summary>
        /// <para>Function call parameters.</para>
        /// <para>Must be specified in case of non-deploy message.</para>
        /// <para>In case of deploy message it is optional and contains parameters</para>
        /// <para>of the functions that will to be called upon deploy transaction.</para>
        /// </summary>
        [JsonPropertyName("call_set")]
        public CallSet CallSet { get; set; }

        /// <summary>
        /// <para>Value in nanotokens to be sent with message.</para>
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// <para>Flag of bounceable message.</para>
        /// <para>Default is true.</para>
        /// </summary>
        [JsonPropertyName("bounce")]
        public bool? Bounce { get; set; }

        /// <summary>
        /// <para>Enable Instant Hypercube Routing for the message.</para>
        /// <para>Default is false.</para>
        /// </summary>
        [JsonPropertyName("enable_ihr")]
        public bool? EnableIhr { get; set; }
    }
}