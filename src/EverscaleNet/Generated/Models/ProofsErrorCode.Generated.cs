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
    public enum ProofsErrorCode
    {
        InvalidData = 901,
        ProofCheckFailed = 902,
        InternalError = 903,
        DataDiffersFromProven = 904
    }
}