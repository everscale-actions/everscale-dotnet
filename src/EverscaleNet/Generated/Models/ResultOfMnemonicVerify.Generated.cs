using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfMnemonicVerify
    {
        /// <summary>
        /// <para>Flag indicating if the mnemonic is valid or not</para>
        /// </summary>
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
    }
}