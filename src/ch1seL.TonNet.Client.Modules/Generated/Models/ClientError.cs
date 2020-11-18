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
    public class ClientError
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("code")]
        public uint Code { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("data")]
        public JsonElement? Data { get; set; }
    }
}