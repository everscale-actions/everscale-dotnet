using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class MnemonicFromEntropyResponse
    {
        /// <summary>
        ///  Phrase
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }
    }
}