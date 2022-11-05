using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfFindLastShardBlock
    {
        /// <summary>
        /// <para>Account address</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}