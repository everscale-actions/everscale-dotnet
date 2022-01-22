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
    public enum ProcessingErrorCode
    {
        MessageAlreadyExpired = 501,
        MessageHasNotDestinationAddress = 502,
        CanNotBuildMessageCell = 503,
        FetchBlockFailed = 504,
        SendMessageFailed = 505,
        InvalidMessageBoc = 506,
        MessageExpired = 507,
        TransactionWaitTimeout = 508,
        InvalidBlockReceived = 509,
        CanNotCheckBlockShard = 510,
        BlockNotFound = 511,
        InvalidData = 512,
        ExternalSignerMustNotBeUsed = 513
    }
}