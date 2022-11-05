using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncodeAccount
    {
        /// <summary>
        /// <para>Account BOC encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// <para>Account ID  encoded in `hex`.</para>
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}