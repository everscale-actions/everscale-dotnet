using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Crypto config.
    /// </summary>
    public class CryptoConfig
    {
        /// <summary>
        /// Mnemonic dictionary that will be used by default in crypto functions. If not specified, 1 dictionary will be used.
        /// </summary>
        [JsonPropertyName("mnemonic_dictionary")]
        public byte? MnemonicDictionary { get; set; }

        /// <summary>
        /// Mnemonic word count that will be used by default in crypto functions. If not specified the default value will be 12.
        /// </summary>
        [JsonPropertyName("mnemonic_word_count")]
        public byte? MnemonicWordCount { get; set; }

        /// <summary>
        /// Derivation path that will be used by default in crypto functions. If not specified `m/44'/396'/0'/0/0` will be used.
        /// </summary>
        [JsonPropertyName("hdkey_derivation_path")]
        public string HdkeyDerivationPath { get; set; }
    }
}