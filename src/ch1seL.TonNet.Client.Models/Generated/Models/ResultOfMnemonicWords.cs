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
    public class ResultOfMnemonicWords
    {
        /// <summary>
        ///  The list of mnemonic words
        /// </summary>
        [JsonPropertyName("words")]
        public string Words { get; set; }
    }
}