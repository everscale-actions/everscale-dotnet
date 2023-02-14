using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CipherMode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CBC,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CFB,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CTR,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ECB,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        OFB
    }
}