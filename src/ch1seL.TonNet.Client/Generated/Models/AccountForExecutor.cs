using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class AccountForExecutor
    {
        public class None : AccountForExecutor
        {
        }

        public class Uninit : AccountForExecutor
        {
        }

        public class Account : AccountForExecutor
        {
            [JsonPropertyName("boc")]
            public string Boc { get; set; }
            [JsonPropertyName("unlimited_balance")]
            public bool UnlimitedBalance { get; set; }
        }
    }
}