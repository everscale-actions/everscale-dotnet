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
    public class ParamsOfProcessMessage
    {
        /// <summary>
        /// Message encode parameters.
        /// </summary>
        [JsonPropertyName("message_encode_params")]
        public ParamsOfEncodeMessage MessageEncodeParams { get; set; }

        /// <summary>
        /// Flag for requesting events sending
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}