using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetSignatureData
    {
        /// <summary>
        /// <para>Contract ABI used to decode.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Message BOC encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}