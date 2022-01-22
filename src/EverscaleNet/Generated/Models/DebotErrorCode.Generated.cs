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
    public enum DebotErrorCode
    {
        DebotStartFailed = 801,
        DebotFetchFailed = 802,
        DebotExecutionFailed = 803,
        DebotInvalidHandle = 804,
        DebotInvalidJsonParams = 805,
        DebotInvalidFunctionId = 806,
        DebotInvalidAbi = 807,
        DebotGetMethodFailed = 808,
        DebotInvalidMsg = 809,
        DebotExternalCallFailed = 810,
        DebotBrowserCallbackFailed = 811,
        DebotOperationRejected = 812,
        DebotNoCode = 813
    }
}