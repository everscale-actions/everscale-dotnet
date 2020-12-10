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
    public class ParamsOfParseShardstate
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int WorkchainId { get; set; }
    }
}