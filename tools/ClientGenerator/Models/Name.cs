using System.Runtime.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public enum Name
    {
        [EnumMember(Value = "callback")] Callback,
        [EnumMember(Value = "context")] Context,
        [EnumMember(Value = "_context")] AltContext,
        [EnumMember(Value = "params")] Params,
        [EnumMember(Value = "request")] Request,
        [EnumMember(Value = "app_object")] AppObject
    }
}