using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ResultOfNaclSignDetached
    {
        /// <summary>
        /// Signature encoded in `hex`.
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}