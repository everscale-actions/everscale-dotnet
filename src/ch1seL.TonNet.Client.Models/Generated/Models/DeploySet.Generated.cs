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
    public class DeploySet
    {
        /// <summary>
        /// Content of TVC file encoded in `base64`.
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }

        /// <summary>
        /// <para>Target workchain for destination address.</para>
        /// <para>Default is `0`.</para>
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int? WorkchainId { get; set; }

        /// <summary>
        /// List of initial values for contract's public variables.
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }

        /// <summary>
        /// <para>Optional public key that can be provided in deploy set in order to substitute one in TVM file or provided by Signer.</para>
        /// <para>Public key resolving priority:</para>
        /// <para>1. Public key from deploy set.</para>
        /// <para>2. Public key, specified in TVM file.</para>
        /// <para>3. Public key, provided by Signer.</para>
        /// </summary>
        [JsonPropertyName("initial_pubkey")]
        public string InitialPubkey { get; set; }
    }
}