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
    public class ParamsOfProcessMessage
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_encode_params")]
        public ParamsOfEncodeMessage MessageEncodeParams { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}