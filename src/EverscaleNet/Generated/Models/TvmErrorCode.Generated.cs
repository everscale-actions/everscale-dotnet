using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum TvmErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotReadTransaction = 401,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotReadBlockchainConfig = 402,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        TransactionAborted = 403,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InternalError = 404,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ActionPhaseFailed = 405,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AccountCodeMissing = 406,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        LowBalance = 407,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AccountFrozenOrDeleted = 408,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AccountMissing = 409,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        UnknownExecutionError = 410,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidInputStack = 411,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidAccountBoc = 412,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidMessageType = 413,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ContractExecutionError = 414
    }
}