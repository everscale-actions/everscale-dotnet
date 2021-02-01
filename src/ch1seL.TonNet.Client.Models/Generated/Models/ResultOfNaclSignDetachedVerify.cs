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
    public class ResultOfNaclSignDetachedVerify
    {
        /// <summary>
        /// `true` if verification succeeded or `false` if it failed
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }
    }
}