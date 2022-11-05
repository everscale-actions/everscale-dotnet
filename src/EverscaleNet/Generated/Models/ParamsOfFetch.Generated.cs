using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Parameters to fetch DeBot metadata.</para>
    /// </summary>
    public class ParamsOfFetch
    {
        /// <summary>
        /// <para>Debot smart contract address.</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}