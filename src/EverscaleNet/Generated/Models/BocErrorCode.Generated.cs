using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum BocErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidBoc = 201,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SerializationError = 202,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InappropriateBlock = 203,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MissingSourceBoc = 204,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InsufficientCacheSize = 205,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        BocRefNotFound = 206,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidBocRef = 207
    }
}