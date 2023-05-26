using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class TransactionFees
    {
        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as ext_in_msg_fee field</para>
        /// </summary>
        [JsonPropertyName("in_msg_fwd_fee")]
        public BigInteger InMsgFwdFee { get; set; }

        /// <summary>
        /// <para>Fee for account storage</para>
        /// </summary>
        [JsonPropertyName("storage_fee")]
        public BigInteger StorageFee { get; set; }

        /// <summary>
        /// <para>Fee for processing</para>
        /// </summary>
        [JsonPropertyName("gas_fee")]
        public BigInteger GasFee { get; set; }

        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as total_fwd_fees field. Deprecated because of its confusing name, that is not the same with GraphQL API Transaction type's field.</para>
        /// </summary>
        [JsonPropertyName("out_msgs_fwd_fee")]
        public BigInteger OutMsgsFwdFee { get; set; }

        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>Contains the same data as account_fees field</para>
        /// </summary>
        [JsonPropertyName("total_account_fees")]
        public BigInteger TotalAccountFees { get; set; }

        /// <summary>
        /// <para>Deprecated because it means total value sent in the transaction, which does not relate to any fees.</para>
        /// </summary>
        [JsonPropertyName("total_output")]
        public BigInteger TotalOutput { get; set; }

        /// <summary>
        /// <para>Fee for inbound external message import.</para>
        /// </summary>
        [JsonPropertyName("ext_in_msg_fee")]
        public BigInteger ExtInMsgFee { get; set; }

        /// <summary>
        /// <para>Total fees the account pays for message forwarding</para>
        /// </summary>
        [JsonPropertyName("total_fwd_fees")]
        public BigInteger TotalFwdFees { get; set; }

        /// <summary>
        /// <para>Total account fees for the transaction execution. Compounds of storage_fee + gas_fee + ext_in_msg_fee + total_fwd_fees</para>
        /// </summary>
        [JsonPropertyName("account_fees")]
        public BigInteger AccountFees { get; set; }
    }
}