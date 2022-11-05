using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfResumeTransactionIterator
    {
        /// <summary>
        /// <para>Iterator state from which to resume.</para>
        /// <para>Same as value returned from `iterator_next`.</para>
        /// </summary>
        [JsonPropertyName("resume_state")]
        public JsonElement? ResumeState { get; set; }

        /// <summary>
        /// <para>Account address filter.</para>
        /// <para>Application can specify the list of accounts for which</para>
        /// <para>it wants to iterate transactions.</para>
        /// <para>If this parameter is missing or an empty list then the library iterates</para>
        /// <para>transactions for all accounts that passes the shard filter.</para>
        /// <para>Note that the library doesn't detect conflicts between the account filter and the shard filter</para>
        /// <para>if both are specified.</para>
        /// <para>So it is the application's responsibility to specify the correct filter combination.</para>
        /// </summary>
        [JsonPropertyName("accounts_filter")]
        public string[] AccountsFilter { get; set; }
    }
}