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
    public enum AbiErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        RequiredAddressMissingForEncodeMessage = 301,
        /// <summary>
        /// Not described yet..
        /// </summary>
        RequiredCallSetMissingForEncodeMessage = 302,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidJson = 303,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidMessage = 304,
        /// <summary>
        /// Not described yet..
        /// </summary>
        EncodeDeployMessageFailed = 305,
        /// <summary>
        /// Not described yet..
        /// </summary>
        EncodeRunMessageFailed = 306,
        /// <summary>
        /// Not described yet..
        /// </summary>
        AttachSignatureFailed = 307,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidTvcImage = 308,
        /// <summary>
        /// Not described yet..
        /// </summary>
        RequiredPublicKeyMissingForFunctionHeader = 309,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidSigner = 310,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidAbi = 311,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidFunctionId = 312,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidData = 313,
        /// <summary>
        /// Not described yet..
        /// </summary>
        EncodeInitialDataFailed = 314
    }
}