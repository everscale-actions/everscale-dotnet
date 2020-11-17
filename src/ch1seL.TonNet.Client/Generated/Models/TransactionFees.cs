using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class TransactionFees
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("in_msg_fwd_fee")]
        public BigInteger InMsgFwdFee { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("storage_fee")]
        public BigInteger StorageFee { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("gas_fee")]
        public BigInteger GasFee { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("out_msgs_fwd_fee")]
        public BigInteger OutMsgsFwdFee { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("total_account_fees")]
        public BigInteger TotalAccountFees { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("total_output")]
        public BigInteger TotalOutput { get; set; }
    }
}