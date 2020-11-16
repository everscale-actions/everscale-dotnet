using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class Abi
    {
        public class Contract : Abi
        {
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }

        public class Json : Abi
        {
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        public class Handle : Abi
        {
            [JsonPropertyName("value")]
            public uint Value { get; set; }
        }

        public class Serialized : Abi
        {
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }
    }
}