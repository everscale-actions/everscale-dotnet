using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum ProofsErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidData = 901,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ProofCheckFailed = 902,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InternalError = 903,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DataDiffersFromProven = 904
    }
}