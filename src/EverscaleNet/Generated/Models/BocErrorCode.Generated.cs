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
    public enum BocErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidBoc = 201,
        /// <summary>
        /// Not described yet..
        /// </summary>
        SerializationError = 202,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InappropriateBlock = 203,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MissingSourceBoc = 204,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InsufficientCacheSize = 205,
        /// <summary>
        /// Not described yet..
        /// </summary>
        BocRefNotFound = 206,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidBocRef = 207
    }
}