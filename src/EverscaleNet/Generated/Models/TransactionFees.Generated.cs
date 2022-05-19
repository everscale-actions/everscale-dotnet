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
        /// <para>Left for backward compatibility. Does not participate in account transaction fees calculation.</para>
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
        /// <para>This is the field that is named as `total_fees` in GraphQL API Transaction type. `total_account_fees` name is misleading, because it does not mean account fees, instead it means</para>
        /// <para>validators total fees received for the transaction execution. It does not include some forward fees that account</para>
        /// <para>actually pays now, but validators will receive later during value delivery to another account (not even in the receiving</para>
        /// <para>transaction).</para>
        /// <para>Because of all of this, this field is not interesting for those who wants to understand</para>
        /// <para>the real account fees, this is why it is deprecated and left for backward compatibility.</para>
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