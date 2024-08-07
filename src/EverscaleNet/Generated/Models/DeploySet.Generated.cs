using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class DeploySet
    {
        /// <summary>
        /// <para>Content of TVC file encoded in `base64`. For compatibility reason this field can contain an encoded  `StateInit`.</para>
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }

        /// <summary>
        /// <para>Contract code BOC encoded with base64.</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// <para>State init BOC encoded with base64.</para>
        /// </summary>
        [JsonPropertyName("state_init")]
        public string StateInit { get; set; }

        /// <summary>
        /// <para>Target workchain for destination address.</para>
        /// <para>Default is `0`.</para>
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int? WorkchainId { get; set; }

        /// <summary>
        /// <para>List of initial values for contract's public variables.</para>
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }

        /// <summary>
        /// <para>Optional public key that can be provided in deploy set in order to substitute one in TVM file or provided by Signer.</para>
        /// <para>Public key resolving priority:</para>
        /// <para>1. Public key from deploy set.</para>
        /// <para>2. Public key, specified in TVM file.</para>
        /// <para>3. Public key, provided by Signer.</para>
        /// <para>Applicable only for contracts with ABI version &lt; 2.4. Contract initial public key should be</para>
        /// <para>explicitly provided inside `initial_data` since ABI 2.4</para>
        /// </summary>
        [JsonPropertyName("initial_pubkey")]
        public string InitialPubkey { get; set; }
    }
}