using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfMnemonicFromRandom
    {
        /// <summary>
        /// <para>Dictionary identifier</para>
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte? Dictionary { get; set; }

        /// <summary>
        /// <para>Mnemonic word count</para>
        /// </summary>
        [JsonPropertyName("word_count")]
        public byte? WordCount { get; set; }
    }
}