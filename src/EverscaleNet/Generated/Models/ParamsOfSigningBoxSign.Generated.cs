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
    public class ParamsOfSigningBoxSign
    {
        /// <summary>
        /// Signing Box handle.
        /// </summary>
        [JsonPropertyName("signing_box")]
        public uint SigningBox { get; set; }

        /// <summary>
        /// <para>Unsigned user data.</para>
        /// <para>Must be encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
    }
}