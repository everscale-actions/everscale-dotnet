using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// 

    /// </summary>
    public enum BocErrorCode
    {
        InvalidBoc = 201,
        SerializationError = 202,
        InappropriateBlock = 203,
        MissingSourceBoc = 204,
        InsufficientCacheSize = 205,
        BocRefNotFound = 206,
        InvalidBocRef = 207
    }
}