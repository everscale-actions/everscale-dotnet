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
    public enum ProcessingErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        MessageAlreadyExpired = 501,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MessageHasNotDestinationAddress = 502,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotBuildMessageCell = 503,
        /// <summary>
        /// Not described yet..
        /// </summary>
        FetchBlockFailed = 504,
        /// <summary>
        /// Not described yet..
        /// </summary>
        SendMessageFailed = 505,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidMessageBoc = 506,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MessageExpired = 507,
        /// <summary>
        /// Not described yet..
        /// </summary>
        TransactionWaitTimeout = 508,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidBlockReceived = 509,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotCheckBlockShard = 510,
        /// <summary>
        /// Not described yet..
        /// </summary>
        BlockNotFound = 511,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidData = 512,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ExternalSignerMustNotBeUsed = 513,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MessageRejected = 514,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidRempStatus = 515,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NextRempStatusTimeout = 516
    }
}