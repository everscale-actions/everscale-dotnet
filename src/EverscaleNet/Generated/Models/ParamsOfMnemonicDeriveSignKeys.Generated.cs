using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfMnemonicDeriveSignKeys
    {
        /// <summary>
        /// <para>Phrase</para>
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }

        /// <summary>
        /// <para>Derivation path, for instance "m/44'/396'/0'/0/0"</para>
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// <para>Dictionary identifier</para>
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte? Dictionary { get; set; }

        /// <summary>
        /// <para>Word count</para>
        /// </summary>
        [JsonPropertyName("word_count")]
        public byte? WordCount { get; set; }
    }
}