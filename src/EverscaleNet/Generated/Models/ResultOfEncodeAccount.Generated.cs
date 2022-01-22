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
    public class ResultOfEncodeAccount
    {
        /// <summary>
        /// Account BOC encoded in `base64`.
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// Account ID  encoded in `hex`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}