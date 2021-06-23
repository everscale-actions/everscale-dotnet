using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Returning values from signing box callbacks.
    /// </summary>
    public abstract class ResultOfAppEncryptionBox
    {
        /// <summary>
        /// Result of getting encryption box info
        /// </summary>
        [JsonDiscriminator("GetInfo")]
        public class GetInfo : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// Result of getting encryption box info
            /// </summary>
            [JsonPropertyName("info")]
            public EncryptionBoxInfo Info { get; set; }
        }

        /// <summary>
        /// Result of encrypting data
        /// </summary>
        [JsonDiscriminator("Encrypt")]
        public class Encrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// Result of encrypting data
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }

        /// <summary>
        /// Result of decrypting data
        /// </summary>
        [JsonDiscriminator("Decrypt")]
        public class Decrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// Result of decrypting data
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }
    }
}