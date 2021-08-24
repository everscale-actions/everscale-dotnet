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
    public enum CipherMode
    {
        CBC,
        CFB,
        CTR,
        ECB,
        OFB
    }
}