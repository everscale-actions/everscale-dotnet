using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfMnemonicFromRandom
    {
        /// <summary>
        /// <para>String of mnemonic words</para>
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }
    }
}