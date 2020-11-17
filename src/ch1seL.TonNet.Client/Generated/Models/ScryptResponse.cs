using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ScryptResponse
    {
        /// <summary>
        ///  Derived key. Encoded with `hex`.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}