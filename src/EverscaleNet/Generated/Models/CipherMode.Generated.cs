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
    public enum CipherMode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        CBC,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CFB,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CTR,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ECB,
        /// <summary>
        /// Not described yet..
        /// </summary>
        OFB
    }
}