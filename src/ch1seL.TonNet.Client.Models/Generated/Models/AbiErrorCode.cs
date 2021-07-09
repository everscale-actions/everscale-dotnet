using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// 

    /// </summary>
    public enum AbiErrorCode
    {
        RequiredAddressMissingForEncodeMessage = 301,
        RequiredCallSetMissingForEncodeMessage = 302,
        InvalidJson = 303,
        InvalidMessage = 304,
        EncodeDeployMessageFailed = 305,
        EncodeRunMessageFailed = 306,
        AttachSignatureFailed = 307,
        InvalidTvcImage = 308,
        RequiredPublicKeyMissingForFunctionHeader = 309,
        InvalidSigner = 310,
        InvalidAbi = 311,
        InvalidFunctionId = 312,
        InvalidData = 313
    }
}