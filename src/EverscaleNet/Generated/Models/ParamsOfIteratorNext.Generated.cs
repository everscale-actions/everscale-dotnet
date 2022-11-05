using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfIteratorNext
    {
        /// <summary>
        /// <para>Iterator handle</para>
        /// </summary>
        [JsonPropertyName("iterator")]
        public uint Iterator { get; set; }

        /// <summary>
        /// <para>Maximum count of the returned items.</para>
        /// <para>If value is missing or is less than 1 the library uses 1.</para>
        /// </summary>
        [JsonPropertyName("limit")]
        public uint? Limit { get; set; }

        /// <summary>
        /// <para>Indicates that function must return the iterator state that can be used for resuming iteration.</para>
        /// </summary>
        [JsonPropertyName("return_resume_state")]
        public bool? ReturnResumeState { get; set; }
    }
}