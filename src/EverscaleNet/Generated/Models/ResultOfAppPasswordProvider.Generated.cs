using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class ResultOfAppPasswordProvider
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("GetPassword")]
        public class GetPassword : ResultOfAppPasswordProvider
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("encrypted_password")]
            public string EncryptedPassword { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("app_encryption_pubkey")]
            public string AppEncryptionPubkey { get; set; }
        }
    }
}