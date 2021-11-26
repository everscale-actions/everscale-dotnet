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
    public class ParamsOfCreateTransactionIterator
    {
        /// <summary>
        /// <para>Starting time to iterate from.</para>
        /// <para>If the application specifies this parameter then the iteration</para>
        /// <para>includes blocks with `gen_utime` &gt;= `start_time`.</para>
        /// <para>Otherwise the iteration starts from zero state.</para>
        /// <para>Must be specified in seconds.</para>
        /// </summary>
        [JsonPropertyName("start_time")]
        public uint? StartTime { get; set; }

        /// <summary>
        /// <para>Optional end time to iterate for.</para>
        /// <para>If the application specifies this parameter then the iteration</para>
        /// <para>includes blocks with `gen_utime` &lt; `end_time`.</para>
        /// <para>Otherwise the iteration never stops.</para>
        /// <para>Must be specified in seconds.</para>
        /// </summary>
        [JsonPropertyName("end_time")]
        public uint? EndTime { get; set; }

        /// <summary>
        /// <para>Shard prefix filters.</para>
        /// <para>If the application specifies this parameter and it is not an empty array</para>
        /// <para>then the iteration will include items related to accounts that belongs to</para>
        /// <para>the specified shard prefixes.</para>
        /// <para>Shard prefix must be represented as a string "workchain:prefix".</para>
        /// <para>Where `workchain` is a signed integer and the `prefix` if a hexadecimal</para>
        /// <para>representation if the 64-bit unsigned integer with tagged shard prefix.</para>
        /// <para>For example: "0:3800000000000000".</para>
        /// <para>Account address conforms to the shard filter if</para>
        /// <para>it belongs to the filter workchain and the first bits of address match to</para>
        /// <para>the shard prefix. Only transactions with suitable account addresses are iterated.</para>
        /// </summary>
        [JsonPropertyName("shard_filter")]
        public string[] ShardFilter { get; set; }

        /// <summary>
        /// <para>Account address filter.</para>
        /// <para>Application can specify the list of accounts for which</para>
        /// <para>it wants to iterate transactions.</para>
        /// <para>If this parameter is missing or an empty list then the library iterates</para>
        /// <para>transactions for all accounts that pass the shard filter.</para>
        /// <para>Note that the library doesn't detect conflicts between the account filter and the shard filter</para>
        /// <para>if both are specified.</para>
        /// <para>So it is an application responsibility to specify the correct filter combination.</para>
        /// </summary>
        [JsonPropertyName("accounts_filter")]
        public string[] AccountsFilter { get; set; }

        /// <summary>
        /// <para>Projection (result) string.</para>
        /// <para>List of the fields that must be returned for iterated items.</para>
        /// <para>This field is the same as the `result` parameter of</para>
        /// <para>the `query_collection` function.</para>
        /// <para>Note that iterated items can contain additional fields that are</para>
        /// <para>not requested in the `result`.</para>
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// <para>Include `transfers` field in iterated transactions.</para>
        /// <para>If this parameter is `true` then each transaction contains field</para>
        /// <para>`transfers` with list of transfer. See more about this structure in function description.</para>
        /// </summary>
        [JsonPropertyName("include_transfers")]
        public bool? IncludeTransfers { get; set; }
    }
}