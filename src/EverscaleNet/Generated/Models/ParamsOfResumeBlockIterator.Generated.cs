using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfResumeBlockIterator
    {
        /// <summary>
        /// <para>Iterator state from which to resume.</para>
        /// <para>Same as value returned from `iterator_next`.</para>
        /// </summary>
        [JsonPropertyName("resume_state")]
        public JsonElement? ResumeState { get; set; }
    }
}