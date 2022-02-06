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
    public enum ClientErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        NotImplemented = 1,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidHex = 2,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidBase64 = 3,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidAddress = 4,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CallbackParamsCantBeConvertedToJson = 5,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WebsocketConnectError = 6,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WebsocketReceiveError = 7,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WebsocketSendError = 8,
        /// <summary>
        /// Not described yet..
        /// </summary>
        HttpClientCreateError = 9,
        /// <summary>
        /// Not described yet..
        /// </summary>
        HttpRequestCreateError = 10,
        /// <summary>
        /// Not described yet..
        /// </summary>
        HttpRequestSendError = 11,
        /// <summary>
        /// Not described yet..
        /// </summary>
        HttpRequestParseError = 12,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CallbackNotRegistered = 13,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NetModuleNotInit = 14,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidConfig = 15,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotCreateRuntime = 16,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidContextHandle = 17,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotSerializeResult = 18,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotSerializeError = 19,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotConvertJsValueToJson = 20,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotReceiveSpawnedResult = 21,
        /// <summary>
        /// Not described yet..
        /// </summary>
        SetTimerError = 22,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidParams = 23,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ContractsAddressConversionFailed = 24,
        /// <summary>
        /// Not described yet..
        /// </summary>
        UnknownFunction = 25,
        /// <summary>
        /// Not described yet..
        /// </summary>
        AppRequestError = 26,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NoSuchRequest = 27,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotSendRequestResult = 28,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotReceiveRequestResult = 29,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotParseRequestResult = 30,
        /// <summary>
        /// Not described yet..
        /// </summary>
        UnexpectedCallbackResponse = 31,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CanNotParseNumber = 32,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InternalError = 33,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidHandle = 34,
        /// <summary>
        /// Not described yet..
        /// </summary>
        LocalStorageError = 35
    }
}