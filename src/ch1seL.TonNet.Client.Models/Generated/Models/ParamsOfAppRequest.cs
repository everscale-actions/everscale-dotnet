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
    public class ParamsOfAppRequest
    {
        /// <summary>
        /// Should be used in `resolve_app_request` call
        /// </summary>
        [JsonPropertyName("app_request_id")]
        public uint AppRequestId { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("request_data")]
        public JsonElement? RequestData { get; set; }
    }
}