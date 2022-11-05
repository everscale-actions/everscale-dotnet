using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfMnemonicWords
    {
        /// <summary>
        /// <para>The list of mnemonic words</para>
        /// </summary>
        [JsonPropertyName("words")]
        public string Words { get; set; }
    }
}