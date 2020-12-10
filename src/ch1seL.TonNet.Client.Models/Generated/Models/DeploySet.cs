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
    public class DeploySet
    {
        /// <summary>
        /// Content of TVC file encoded in `base64`.
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }

        /// <summary>
        /// <para>Target workchain for destination address.</para>
        /// <para>Default is `0`.</para>
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int? WorkchainId { get; set; }

        /// <summary>
        /// List of initial values for contract's public variables.
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }
    }
}