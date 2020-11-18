using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ResultOfGetBocHash
    {
        /// <summary>
        ///  BOC root hash encoded with hex
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}