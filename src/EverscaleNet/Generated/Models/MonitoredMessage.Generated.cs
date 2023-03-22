using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Boc), nameof(Boc))]
    [JsonDerivedType(typeof(HashAddress), nameof(HashAddress))]
#endif
    public abstract class MonitoredMessage
    {
        /// <summary>
        /// <para>BOC of the message.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Boc")]
#endif
        public class Boc : MonitoredMessage
        {
            /// <summary>
            /// <para>BOC of the message.</para>
            /// </summary>
            [JsonPropertyName("boc")]
            public string BocAccessor { get; set; }
        }

        /// <summary>
        /// <para>Message's hash and destination address.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("HashAddress")]
#endif
        public class HashAddress : MonitoredMessage
        {
            /// <summary>
            /// <para>Message's hash and destination address.</para>
            /// </summary>
            [JsonPropertyName("hash")]
            public string Hash { get; set; }

            /// <summary>
            /// <para>Message's hash and destination address.</para>
            /// </summary>
            [JsonPropertyName("address")]
            public string Address { get; set; }
        }
    }
}