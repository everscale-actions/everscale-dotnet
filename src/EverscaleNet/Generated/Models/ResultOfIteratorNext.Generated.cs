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
    public class ResultOfIteratorNext
    {
        /// <summary>
        /// <para>Next available items.</para>
        /// <para>Note that `iterator_next` can return an empty items and `has_more` equals to `true`.</para>
        /// <para>In this case the application have to continue iteration.</para>
        /// <para>Such situation can take place when there is no data yet but</para>
        /// <para>the requested `end_time` is not reached.</para>
        /// </summary>
        [JsonPropertyName("items")]
        public JsonElement[] Items { get; set; }

        /// <summary>
        /// Indicates that there are more available items in iterated range.
        /// </summary>
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// <para>Optional iterator state that can be used for resuming iteration.</para>
        /// <para>This field is returned only if the `return_resume_state` parameter</para>
        /// <para>is specified.</para>
        /// <para>Note that `resume_state` corresponds to the iteration position</para>
        /// <para>after the returned items.</para>
        /// </summary>
        [JsonPropertyName("resume_state")]
        public JsonElement? ResumeState { get; set; }
    }
}