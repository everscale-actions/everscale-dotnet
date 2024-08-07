using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    public abstract class ParamsOfQueryOperation
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("QueryCollection")]
        public ParamsOfQueryCollection QueryCollection { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("WaitForCollection")]
        public ParamsOfWaitForCollection WaitForCollection { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("AggregateCollection")]
        public ParamsOfAggregateCollection AggregateCollection { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("QueryCounterparties")]
        public ParamsOfQueryCounterparties QueryCounterparties { get; set; }
    }
}