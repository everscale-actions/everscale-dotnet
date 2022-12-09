using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfQueryTransactionTree
    {
        /// <summary>
        /// <para>Input message id.</para>
        /// </summary>
        [JsonPropertyName("in_msg")]
        public string InMsg { get; set; }

        /// <summary>
        /// <para>List of contract ABIs that will be used to decode message bodies. Library will try to decode each returned message body using any ABI from the registry.</para>
        /// </summary>
        [JsonPropertyName("abi_registry")]
        public Abi[] AbiRegistry { get; set; }

        /// <summary>
        /// <para>Timeout used to limit waiting time for the missing messages and transaction.</para>
        /// <para>If some of the following messages and transactions are missing yet</para>
        /// <para>The maximum waiting time is regulated by this option.</para>
        /// <para>Default value is 60000 (1 min). If `timeout` is set to 0 then function will wait infinitely</para>
        /// <para>until the whole transaction tree is executed</para>
        /// </summary>
        [JsonPropertyName("timeout")]
        public uint? Timeout { get; set; }

        /// <summary>
        /// <para>Maximum transaction count to wait.</para>
        /// <para>If transaction tree contains more transaction then this parameter then only first `transaction_max_count` transaction are awaited and returned.</para>
        /// <para>Default value is 50. If `transaction_max_count` is set to 0 then no limitation on</para>
        /// <para>transaction count is used and all transaction are returned.</para>
        /// </summary>
        [JsonPropertyName("transaction_max_count")]
        public uint? TransactionMaxCount { get; set; }
    }
}