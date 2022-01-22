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

        /// <summary>
        /// <para>Timeout used to limit waiting time for the missing messages and transaction.</para>
        /// <para>If some of the following messages and transactions are missing yet</para>
        /// <para>The maximum waiting time is regulated by this option.</para>
        /// <para>Default value is 60000 (1 min).</para>
        /// </summary>
        [JsonPropertyName("timeout")]
        public uint? Timeout { get; set; }
    }
}