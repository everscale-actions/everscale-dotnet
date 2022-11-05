using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetCodeFromTvc
    {
        /// <summary>
        /// <para>Contract TVC image or image BOC handle</para>
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }
    }
}