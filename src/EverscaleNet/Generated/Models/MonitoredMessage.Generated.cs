using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Boc), nameof(Boc))]
    [JsonDerivedType(typeof(HashAddress), nameof(HashAddress))]
    public abstract class MonitoredMessage
    {
        /// <summary>
        /// <para>BOC of the message.</para>
        /// </summary>
        public class Boc : MonitoredMessage
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("boc")]
            public string BocAccessor { get; set; }
        }

        /// <summary>
        /// <para>Message's hash and destination address.</para>
        /// </summary>
        public class HashAddress : MonitoredMessage
        {
            /// <summary>
            /// <para>Hash of the message.</para>
            /// </summary>
            [JsonPropertyName("hash")]
            public string Hash { get; set; }

            /// <summary>
            /// <para>Destination address of the message.</para>
            /// </summary>
            [JsonPropertyName("address")]
            public string Address { get; set; }
        }
    }
}