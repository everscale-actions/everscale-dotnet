using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Crypto config.</para>
    /// </summary>
    public class CryptoConfig
    {
        /// <summary>
        /// <para>Mnemonic dictionary that will be used by default in crypto functions. If not specified, 1 dictionary will be used.</para>
        /// </summary>
        [JsonPropertyName("mnemonic_dictionary")]
        public byte? MnemonicDictionary { get; set; }

        /// <summary>
        /// <para>Mnemonic word count that will be used by default in crypto functions. If not specified the default value will be 12.</para>
        /// </summary>
        [JsonPropertyName("mnemonic_word_count")]
        public byte? MnemonicWordCount { get; set; }

        /// <summary>
        /// <para>Derivation path that will be used by default in crypto functions. If not specified `m/44'/396'/0'/0/0` will be used.</para>
        /// </summary>
        [JsonPropertyName("hdkey_derivation_path")]
        public string HdkeyDerivationPath { get; set; }
    }
}