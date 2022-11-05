using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfMnemonicWords
    {
        /// <summary>
        /// <para>Dictionary identifier</para>
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte? Dictionary { get; set; }
    }
}