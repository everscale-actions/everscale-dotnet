using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Parameters to init DeBot.</para>
    /// </summary>
    public class ParamsOfInit
    {
        /// <summary>
        /// <para>Debot smart contract address</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}