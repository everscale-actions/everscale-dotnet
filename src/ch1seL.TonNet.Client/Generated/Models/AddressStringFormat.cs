using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class AddressStringFormat
    {
        public class AccountId : AddressStringFormat
        {
        }

        public class Hex : AddressStringFormat
        {
        }

        public class Base64 : AddressStringFormat
        {
            [JsonPropertyName("url")]
            public bool Url { get; set; }
            [JsonPropertyName("test")]
            public bool Test { get; set; }
            [JsonPropertyName("bounce")]
            public bool Bounce { get; set; }
        }
    }
}