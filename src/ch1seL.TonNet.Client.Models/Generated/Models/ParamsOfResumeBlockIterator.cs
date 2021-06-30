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