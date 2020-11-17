using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class MnemonicVerifyResponse
    {
        /// <summary>
        ///  Flag indicating if the mnemonic is valid or not
        /// </summary>
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
    }
}