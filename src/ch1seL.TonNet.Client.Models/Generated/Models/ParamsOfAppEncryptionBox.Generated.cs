using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Encryption box callbacks.
    /// </summary>
    public abstract class ParamsOfAppEncryptionBox
    {
        /// <summary>
        /// Get encryption box info
        /// </summary>
        [JsonDiscriminator("GetInfo")]
        public class GetInfo : ParamsOfAppEncryptionBox
        {
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        [JsonDiscriminator("Encrypt")]
        public class Encrypt : ParamsOfAppEncryptionBox
        {
            /// <summary>
            /// Encrypt data
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        [JsonDiscriminator("Decrypt")]
        public class Decrypt : ParamsOfAppEncryptionBox
        {
            /// <summary>
            /// Decrypt data
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }
    }
}