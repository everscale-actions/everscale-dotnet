using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class TvcV1
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}