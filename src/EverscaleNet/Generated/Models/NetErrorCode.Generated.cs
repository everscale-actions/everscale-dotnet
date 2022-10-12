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
    public enum NetErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        QueryFailed = 601,
        /// <summary>
        /// Not described yet..
        /// </summary>
        SubscribeFailed = 602,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WaitForFailed = 603,
        /// <summary>
        /// Not described yet..
        /// </summary>
        GetSubscriptionResultFailed = 604,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidServerResponse = 605,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ClockOutOfSync = 606,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WaitForTimeout = 607,
        /// <summary>
        /// Not described yet..
        /// </summary>
        GraphqlError = 608,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NetworkModuleSuspended = 609,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WebsocketDisconnected = 610,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NotSupported = 611,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NoEndpointsProvided = 612,
        /// <summary>
        /// Not described yet..
        /// </summary>
        GraphqlWebsocketInitError = 613,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NetworkModuleResumed = 614,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Unauthorized = 615
    }
}