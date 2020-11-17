using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class SubscribeCollectionResponse
    {
        /// <summary>
        ///  Subscription handle. Must be closed with `unsubscribe`
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }
    }
}