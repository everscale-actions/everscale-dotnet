using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// 

    /// </summary>
    public enum ClientErrorCode
    {
        NotImplemented = 1,
        InvalidHex = 2,
        InvalidBase64 = 3,
        InvalidAddress = 4,
        CallbackParamsCantBeConvertedToJson = 5,
        WebsocketConnectError = 6,
        WebsocketReceiveError = 7,
        WebsocketSendError = 8,
        HttpClientCreateError = 9,
        HttpRequestCreateError = 10,
        HttpRequestSendError = 11,
        HttpRequestParseError = 12,
        CallbackNotRegistered = 13,
        NetModuleNotInit = 14,
        InvalidConfig = 15,
        CannotCreateRuntime = 16,
        InvalidContextHandle = 17,
        CannotSerializeResult = 18,
        CannotSerializeError = 19,
        CannotConvertJsValueToJson = 20,
        CannotReceiveSpawnedResult = 21,
        SetTimerError = 22,
        InvalidParams = 23,
        ContractsAddressConversionFailed = 24,
        UnknownFunction = 25,
        AppRequestError = 26,
        NoSuchRequest = 27,
        CanNotSendRequestResult = 28,
        CanNotReceiveRequestResult = 29,
        CanNotParseRequestResult = 30,
        UnexpectedCallbackResponse = 31,
        CanNotParseNumber = 32,
        InternalError = 33,
        InvalidHandle = 34
    }
}