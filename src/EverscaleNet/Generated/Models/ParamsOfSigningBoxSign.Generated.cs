using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfSigningBoxSign
    {
        /// <summary>
        /// <para>Signing Box handle.</para>
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