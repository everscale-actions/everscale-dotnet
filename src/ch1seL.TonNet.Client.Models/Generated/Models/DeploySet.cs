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
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }

        /// <summary>
        /// Default is `0`.
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int? WorkchainId { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }
    }
}