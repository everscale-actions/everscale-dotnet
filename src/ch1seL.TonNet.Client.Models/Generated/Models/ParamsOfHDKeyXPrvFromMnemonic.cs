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
    public class ParamsOfHDKeyXPrvFromMnemonic
    {
        /// <summary>
        /// String with seed phrase
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }

        /// <summary>
        /// Dictionary identifier
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte? Dictionary { get; set; }

        /// <summary>
        /// Mnemonic word count
        /// </summary>
        [JsonPropertyName("word_count")]
        public byte? WordCount { get; set; }
    }
}