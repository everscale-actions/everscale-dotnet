using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfHDKeyPublicFromXPrv
    {
        /// <summary>
        /// <para>Serialized extended private key</para>
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }
    }
}