using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfMnemonicFromEntropy
    {
        /// <summary>
        /// <para>Phrase</para>
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }
    }
}