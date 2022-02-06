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
    public enum DebotErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotStartFailed = 801,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotFetchFailed = 802,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotExecutionFailed = 803,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotInvalidHandle = 804,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotInvalidJsonParams = 805,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotInvalidFunctionId = 806,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotInvalidAbi = 807,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotGetMethodFailed = 808,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotInvalidMsg = 809,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotExternalCallFailed = 810,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotBrowserCallbackFailed = 811,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotOperationRejected = 812,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DebotNoCode = 813
    }
}