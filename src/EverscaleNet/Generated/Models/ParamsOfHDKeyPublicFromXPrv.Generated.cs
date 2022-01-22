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
    public class ParamsOfHDKeyPublicFromXPrv
    {
        /// <summary>
        /// Serialized extended private key
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }
    }
}