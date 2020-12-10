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
    public class DebotAction
    {
        /// <summary>
        /// Should be used by Debot Browser as name ofmenu item.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Can be a debot function name or a print string(for Print Action).
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("action_type")]
        public byte ActionType { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("to")]
        public byte To { get; set; }

        /// <summary>
        /// In the form of "param=value,flag".attribute example: instant, args, fargs, sign.
        /// </summary>
        [JsonPropertyName("attributes")]
        public string Attributes { get; set; }

        /// <summary>
        /// Used by debot only.
        /// </summary>
        [JsonPropertyName("misc")]
        public string Misc { get; set; }
    }
}