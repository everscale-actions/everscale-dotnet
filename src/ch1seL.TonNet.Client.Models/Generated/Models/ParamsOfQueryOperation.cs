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
    public abstract class ParamsOfQueryOperation
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("QueryCollection")]
        public ParamsOfQueryCollection QueryCollection { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("WaitForCollection")]
        public ParamsOfWaitForCollection WaitForCollection { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("AggregateCollection")]
        public ParamsOfAggregateCollection AggregateCollection { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("QueryCounterparties")]
        public ParamsOfQueryCounterparties QueryCounterparties { get; set; }
    }
}