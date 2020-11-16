using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class Signer
    {
        public class None : Signer
        {
        }

        public class External : Signer
        {
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        public class Keys : Signer
        {
            [JsonPropertyName("keys")]
            public KeyPair KeysAccessor { get; set; }
        }

        public class SigningBox : Signer
        {
            [JsonPropertyName("handle")]
            public uint Handle { get; set; }
        }
    }
}