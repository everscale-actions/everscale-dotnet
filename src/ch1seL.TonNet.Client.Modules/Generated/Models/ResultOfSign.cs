using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ResultOfSign
    {
        /// <summary>
        ///  Signed data combined with signature encoded in `base64`.
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }

        /// <summary>
        ///  Signature encoded in `hex`.
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}