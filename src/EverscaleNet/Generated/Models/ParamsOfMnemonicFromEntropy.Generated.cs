using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfMnemonicFromEntropy
    {
        /// <summary>
        /// <para>Entropy bytes.</para>
        /// <para>Hex encoded.</para>
        /// </summary>
        [JsonPropertyName("entropy")]
        public string Entropy { get; set; }

        /// <summary>
        /// <para>Dictionary identifier</para>
        /// </summary>
        [JsonPropertyName("dictionary")]
        public MnemonicDictionary? Dictionary { get; set; }

        /// <summary>
        /// <para>Mnemonic word count</para>
        /// </summary>
        [JsonPropertyName("word_count")]
        public byte? WordCount { get; set; }
    }
}