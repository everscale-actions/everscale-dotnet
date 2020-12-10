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
    public class ParamsOfSigningBoxSign
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("signing_box")]
        public uint SigningBox { get; set; }

        /// <summary>
        /// Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
    }
}