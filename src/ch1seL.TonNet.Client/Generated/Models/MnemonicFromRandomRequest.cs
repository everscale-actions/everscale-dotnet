using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class MnemonicFromRandomRequest
    {
        /// <summary>
        ///  Dictionary identifier
        /// </summary>
        [JsonPropertyName("dictionary"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? Dictionary { get; set; }

        /// <summary>
        ///  Mnemonic word count
        /// </summary>
        [JsonPropertyName("word_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? WordCount { get; set; }
    }
}