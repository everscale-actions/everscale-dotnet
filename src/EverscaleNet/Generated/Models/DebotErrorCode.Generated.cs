using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum DebotErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotStartFailed = 801,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotFetchFailed = 802,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotExecutionFailed = 803,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotInvalidHandle = 804,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotInvalidJsonParams = 805,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotInvalidFunctionId = 806,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotInvalidAbi = 807,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotGetMethodFailed = 808,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotInvalidMsg = 809,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotExternalCallFailed = 810,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotBrowserCallbackFailed = 811,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotOperationRejected = 812,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DebotNoCode = 813
    }
}