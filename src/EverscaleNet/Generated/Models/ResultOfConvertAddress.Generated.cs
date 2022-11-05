using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfConvertAddress
    {
        /// <summary>
        /// <para>Address in the specified format</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}