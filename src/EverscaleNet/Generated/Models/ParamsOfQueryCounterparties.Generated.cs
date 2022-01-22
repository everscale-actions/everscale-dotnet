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
    public class ParamsOfQueryCounterparties
    {
        /// <summary>
        /// Account address
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// Projection (result) string
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// Number of counterparties to return
        /// </summary>
        [JsonPropertyName("first")]
        public uint? First { get; set; }

        /// <summary>
        /// `cursor` field of the last received result
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }
    }
}