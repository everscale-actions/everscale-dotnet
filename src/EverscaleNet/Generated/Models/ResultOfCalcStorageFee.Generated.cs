using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfCalcStorageFee
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("fee")]
        public string Fee { get; set; }
    }
}