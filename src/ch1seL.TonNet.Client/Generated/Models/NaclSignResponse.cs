using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class NaclSignResponse
    {
        /// <summary>
        ///  Signed data, encoded in `base64`.
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }
    }
}