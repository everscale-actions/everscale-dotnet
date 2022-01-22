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
    public class ResultOfScrypt
    {
        /// <summary>
        /// <para>Derived key.</para>
        /// <para>Encoded with `hex`.</para>
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}