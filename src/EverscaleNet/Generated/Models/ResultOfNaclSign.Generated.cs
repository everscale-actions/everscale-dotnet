using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfNaclSign
    {
        /// <summary>
        /// <para>Signed data, encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }
    }
}