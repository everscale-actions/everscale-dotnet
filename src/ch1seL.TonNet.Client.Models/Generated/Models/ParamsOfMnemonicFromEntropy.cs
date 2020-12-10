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
    public class ParamsOfMnemonicFromEntropy
    {
        /// <summary>
        /// Hex encoded.
        /// </summary>
        [JsonPropertyName("entropy")]
        public string Entropy { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte? Dictionary { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("word_count")]
        public byte? WordCount { get; set; }
    }
}