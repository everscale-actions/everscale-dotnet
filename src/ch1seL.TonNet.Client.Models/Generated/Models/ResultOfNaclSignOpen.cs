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
    public class ResultOfNaclSignOpen
    {
        /// <summary>
        ///  Unsigned data, encoded in `base64`.
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
    }
}