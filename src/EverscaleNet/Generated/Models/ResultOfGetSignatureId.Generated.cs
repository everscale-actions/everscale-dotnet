using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetSignatureId
    {
        /// <summary>
        /// <para>Signature ID for configured network if it should be used in messages signature</para>
        /// </summary>
        [JsonPropertyName("signature_id")]
        public int? SignatureId { get; set; }
    }
}