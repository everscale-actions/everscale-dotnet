using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum AbiErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        RequiredAddressMissingForEncodeMessage = 301,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        RequiredCallSetMissingForEncodeMessage = 302,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidJson = 303,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidMessage = 304,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        EncodeDeployMessageFailed = 305,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        EncodeRunMessageFailed = 306,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AttachSignatureFailed = 307,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidTvcImage = 308,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        RequiredPublicKeyMissingForFunctionHeader = 309,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidSigner = 310,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidAbi = 311,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidFunctionId = 312,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidData = 313,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        EncodeInitialDataFailed = 314,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidFunctionName = 315
    }
}