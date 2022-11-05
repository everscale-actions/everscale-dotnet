using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfAppRequest
    {
        /// <summary>
        /// <para>Request ID.</para>
        /// <para>Should be used in `resolve_app_request` call</para>
        /// </summary>
        [JsonPropertyName("app_request_id")]
        public uint AppRequestId { get; set; }

        /// <summary>
        /// <para>Request describing data</para>
        /// </summary>
        [JsonPropertyName("request_data")]
        public JsonElement? RequestData { get; set; }
    }
}