using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class TransactionFees
    {
        [JsonPropertyName("in_msg_fwd_fee")]
        public BigInteger InMsgFwdFee { get; set; }
        [JsonPropertyName("storage_fee")]
        public BigInteger StorageFee { get; set; }
        [JsonPropertyName("gas_fee")]
        public BigInteger GasFee { get; set; }
        [JsonPropertyName("out_msgs_fwd_fee")]
        public BigInteger OutMsgsFwdFee { get; set; }
        [JsonPropertyName("total_account_fees")]
        public BigInteger TotalAccountFees { get; set; }
        [JsonPropertyName("total_output")]
        public BigInteger TotalOutput { get; set; }
    }
}