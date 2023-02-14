using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfHDKeyXPrvFromMnemonic
    {
        /// <summary>
        /// <para>String with seed phrase</para>
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }

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