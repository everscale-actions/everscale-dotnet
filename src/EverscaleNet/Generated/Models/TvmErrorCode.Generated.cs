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
    public enum TvmErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotReadTransaction = 401,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotReadBlockchainConfig = 402,
        /// <summary>
        /// Not described yet..
        /// </summary>
        TransactionAborted = 403,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InternalError = 404,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ActionPhaseFailed = 405,
        /// <summary>
        /// Not described yet..
        /// </summary>
        AccountCodeMissing = 406,
        /// <summary>
        /// Not described yet..
        /// </summary>
        LowBalance = 407,
        /// <summary>
        /// Not described yet..
        /// </summary>
        AccountFrozenOrDeleted = 408,
        /// <summary>
        /// Not described yet..
        /// </summary>
        AccountMissing = 409,
        /// <summary>
        /// Not described yet..
        /// </summary>
        UnknownExecutionError = 410,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidInputStack = 411,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidAccountBoc = 412,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidMessageType = 413,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ContractExecutionError = 414
    }
}