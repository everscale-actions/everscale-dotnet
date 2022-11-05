using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfBatchQuery
    {
        /// <summary>
        /// <para>List of query operations that must be performed per single fetch.</para>
        /// </summary>
        [JsonPropertyName("operations")]
        public ParamsOfQueryOperation[] Operations { get; set; }
    }
}