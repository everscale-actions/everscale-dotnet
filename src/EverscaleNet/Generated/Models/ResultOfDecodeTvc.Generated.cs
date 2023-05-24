using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfDecodeTvc
    {
        /// <summary>
        /// <para>Decoded TVC</para>
        /// </summary>
        [JsonPropertyName("tvc")]
        public Tvc Tvc { get; set; }
    }
}