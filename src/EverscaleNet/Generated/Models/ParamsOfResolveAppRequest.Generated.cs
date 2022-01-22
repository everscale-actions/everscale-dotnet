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
    public class ParamsOfResolveAppRequest
    {
        /// <summary>
        /// Request ID received from SDK
        /// </summary>
        [JsonPropertyName("app_request_id")]
        public uint AppRequestId { get; set; }

        /// <summary>
        /// Result of request processing
        /// </summary>
        [JsonPropertyName("result")]
        public AppRequestResult Result { get; set; }
    }
}