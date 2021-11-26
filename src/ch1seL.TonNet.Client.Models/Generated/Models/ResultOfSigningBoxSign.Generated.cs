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
    public class ResultOfSigningBoxSign
    {
        /// <summary>
        /// <para>Data signature.</para>
        /// <para>Encoded with `hex`.</para>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}