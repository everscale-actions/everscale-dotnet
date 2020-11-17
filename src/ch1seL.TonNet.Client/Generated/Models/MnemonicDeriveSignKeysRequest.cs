using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class MnemonicDeriveSignKeysRequest
    {
        /// <summary>
        ///  Phrase
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }

        /// <summary>
        ///  Derivation path, for instance "m/44'/396'/0'/0/0"
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        ///  Dictionary identifier
        /// </summary>
        [JsonPropertyName("dictionary"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? Dictionary { get; set; }

        /// <summary>
        ///  Word count
        /// </summary>
        [JsonPropertyName("word_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? WordCount { get; set; }
    }
}