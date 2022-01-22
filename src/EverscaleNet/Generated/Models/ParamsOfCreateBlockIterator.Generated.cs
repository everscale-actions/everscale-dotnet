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
    public class ParamsOfCreateBlockIterator
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
        /// <para>Shard prefix filter.</para>
        /// <para>If the application specifies this parameter and it is not the empty array</para>
        /// <para>then the iteration will include items related to accounts that belongs to</para>
        /// <para>the specified shard prefixes.</para>
        /// <para>Shard prefix must be represented as a string "workchain:prefix".</para>
        /// <para>Where `workchain` is a signed integer and the `prefix` if a hexadecimal</para>
        /// <para>representation if the 64-bit unsigned integer with tagged shard prefix.</para>
        /// <para>For example: "0:3800000000000000".</para>
        /// </summary>
        [JsonPropertyName("shard_filter")]
        public string[] ShardFilter { get; set; }

        /// <summary>
        /// <para>Projection (result) string.</para>
        /// <para>List of the fields that must be returned for iterated items.</para>
        /// <para>This field is the same as the `result` parameter of</para>
        /// <para>the `query_collection` function.</para>
        /// <para>Note that iterated items can contains additional fields that are</para>
        /// <para>not requested in the `result`.</para>
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}