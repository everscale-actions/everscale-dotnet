using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfProcessMessage
    {
        /// <summary>
        /// <para>Message encode parameters.</para>
        /// </summary>
        [JsonPropertyName("message_encode_params")]
        public ParamsOfEncodeMessage MessageEncodeParams { get; set; }

        /// <summary>
        /// <para>Flag for requesting events sending. Default is `false`.</para>
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool? SendEvents { get; set; }
    }
}