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
    public class TransactionFees
    {
        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as ext_in_msg_fee field</para>
        /// </summary>
        [JsonPropertyName("in_msg_fwd_fee")]
        public ulong InMsgFwdFee { get; set; }

        /// <summary>
        /// Fee for account storage
        /// </summary>
        [JsonPropertyName("storage_fee")]
        public ulong StorageFee { get; set; }

        /// <summary>
        /// Fee for processing
        /// </summary>
        [JsonPropertyName("gas_fee")]
        public ulong GasFee { get; set; }

        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as total_fwd_fees field. Deprecated because of its confusing name, that is not the same with GraphQL API Transaction type's field.</para>
        /// </summary>
        [JsonPropertyName("out_msgs_fwd_fee")]
        public ulong OutMsgsFwdFee { get; set; }

        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as account_fees field</para>
        /// </summary>
        [JsonPropertyName("total_account_fees")]
        public ulong TotalAccountFees { get; set; }

        /// <summary>
        /// Deprecated because it means total value sent in the transaction, which does not relate to any fees.
        /// </summary>
        [JsonPropertyName("total_output")]
        public ulong TotalOutput { get; set; }

        /// <summary>
        /// Fee for inbound external message import.
        /// </summary>
        [JsonPropertyName("ext_in_msg_fee")]
        public ulong ExtInMsgFee { get; set; }

        /// <summary>
        /// Total fees the account pays for message forwarding
        /// </summary>
        [JsonPropertyName("total_fwd_fees")]
        public ulong TotalFwdFees { get; set; }

        /// <summary>
        /// Total account fees for the transaction execution. Compounds of storage_fee + gas_fee + ext_in_msg_fee + total_fwd_fees
        /// </summary>
        [JsonPropertyName("account_fees")]
        public ulong AccountFees { get; set; }
    }
}