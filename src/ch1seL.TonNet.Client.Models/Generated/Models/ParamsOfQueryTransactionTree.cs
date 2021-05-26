using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ParamsOfQueryTransactionTree
    {
        /// <summary>
        /// Input message id.
        /// </summary>
        [JsonPropertyName("in_msg")]
        public string InMsg { get; set; }

        /// <summary>
        /// List of contract ABIs that will be used to decode message bodies. Library will try to decode each returned message body using any ABI from the registry.
        /// </summary>
        [JsonPropertyName("abi_registry")]
        public Abi[] AbiRegistry { get; set; }
    }
}