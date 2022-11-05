using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ChaCha20ParamsCB
    {
        /// <summary>
        /// <para>96-bit nonce.</para>
        /// <para>Must be encoded with `hex`.</para>
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}