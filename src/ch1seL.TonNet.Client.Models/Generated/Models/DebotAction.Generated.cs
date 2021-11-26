using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Describes a debot action in a Debot Context.
    /// </summary>
    public class DebotAction
    {
        /// <summary>
        /// <para>A short action description.</para>
        /// <para>Should be used by Debot Browser as name of menu item.</para>
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// <para>Depends on action type.</para>
        /// <para>Can be a debot function name or a print string (for Print Action).</para>
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Action type.
        /// </summary>
        [JsonPropertyName("action_type")]
        public byte ActionType { get; set; }

        /// <summary>
        /// ID of debot context to switch after action execution.
        /// </summary>
        [JsonPropertyName("to")]
        public byte To { get; set; }

        /// <summary>
        /// <para>Action attributes.</para>
        /// <para>In the form of "param=value,flag". attribute example: instant, args, fargs, sign.</para>
        /// </summary>
        [JsonPropertyName("attributes")]
        public string Attributes { get; set; }

        /// <summary>
        /// <para>Some internal action data.</para>
        /// <para>Used by debot only.</para>
        /// </summary>
        [JsonPropertyName("misc")]
        public string Misc { get; set; }
    }
}