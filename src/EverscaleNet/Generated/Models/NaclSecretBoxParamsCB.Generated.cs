using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class NaclSecretBoxParamsCB
    {
        /// <summary>
        /// <para>Nonce in `hex`</para>
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}