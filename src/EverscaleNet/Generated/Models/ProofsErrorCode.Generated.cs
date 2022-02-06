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
    public enum ProofsErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidData = 901,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ProofCheckFailed = 902,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InternalError = 903,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DataDiffersFromProven = 904
    }
}