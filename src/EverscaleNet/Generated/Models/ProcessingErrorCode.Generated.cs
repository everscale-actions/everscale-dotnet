using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum ProcessingErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MessageAlreadyExpired = 501,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MessageHasNotDestinationAddress = 502,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotBuildMessageCell = 503,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        FetchBlockFailed = 504,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SendMessageFailed = 505,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidMessageBoc = 506,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MessageExpired = 507,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        TransactionWaitTimeout = 508,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidBlockReceived = 509,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotCheckBlockShard = 510,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        BlockNotFound = 511,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidData = 512,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ExternalSignerMustNotBeUsed = 513,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MessageRejected = 514,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidRempStatus = 515,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NextRempStatusTimeout = 516
    }
}