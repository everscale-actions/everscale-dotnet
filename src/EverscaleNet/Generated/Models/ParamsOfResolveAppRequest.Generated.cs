using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfResolveAppRequest
    {
        /// <summary>
        /// <para>Request ID received from SDK</para>
        /// </summary>
        [JsonPropertyName("app_request_id")]
        public uint AppRequestId { get; set; }

        /// <summary>
        /// <para>Result of request processing</para>
        /// </summary>
        [JsonPropertyName("result")]
        public AppRequestResult Result { get; set; }
    }
}