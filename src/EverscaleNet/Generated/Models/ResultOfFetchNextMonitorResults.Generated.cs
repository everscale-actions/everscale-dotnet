using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfFetchNextMonitorResults
    {
        /// <summary>
        /// <para>List of the resolved results.</para>
        /// </summary>
        [JsonPropertyName("results")]
        public MessageMonitoringResult[] Results { get; set; }
    }
}