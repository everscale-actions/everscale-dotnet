using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum NetErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        QueryFailed = 601,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SubscribeFailed = 602,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WaitForFailed = 603,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        GetSubscriptionResultFailed = 604,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidServerResponse = 605,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ClockOutOfSync = 606,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WaitForTimeout = 607,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        GraphqlError = 608,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NetworkModuleSuspended = 609,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WebsocketDisconnected = 610,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NotSupported = 611,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NoEndpointsProvided = 612,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        GraphqlWebsocketInitError = 613,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NetworkModuleResumed = 614,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Unauthorized = 615,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        QueryTransactionTreeTimeout = 616
    }
}