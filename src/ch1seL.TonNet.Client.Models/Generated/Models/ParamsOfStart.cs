using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    ///  [UNSTABLE](UNSTABLE.md) Parameters to start debot.
    /// </summary>
    public class ParamsOfStart
    {
        /// <summary>
        ///  Debot smart contract address 
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}