using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class HDKeyXPrvFromMnemonicResponse
    {
        /// <summary>
        ///  Serialized extended master private key
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }
    }
}