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
    public class ResultOfNaclSign
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }
    }
}