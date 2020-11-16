using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class StateInitSource
    {
        public class Message : StateInitSource
        {
            [JsonPropertyName("source")]
            public MessageSource Source { get; set; }
        }

        public class StateInit : StateInitSource
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            [JsonPropertyName("data")]
            public string Data { get; set; }
            [JsonPropertyName("library")]
            public string Library { get; set; }
        }

        public class Tvc : StateInitSource
        {
            [JsonPropertyName("tvc")]
            public string TvcAccessor { get; set; }
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
            [JsonPropertyName("init_params")]
            public StateInitParams InitParams { get; set; }
        }
    }
}