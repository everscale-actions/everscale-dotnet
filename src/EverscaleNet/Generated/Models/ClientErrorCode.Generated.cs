using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum ClientErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NotImplemented = 1,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidHex = 2,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidBase64 = 3,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidAddress = 4,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CallbackParamsCantBeConvertedToJson = 5,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WebsocketConnectError = 6,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WebsocketReceiveError = 7,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WebsocketSendError = 8,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        HttpClientCreateError = 9,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        HttpRequestCreateError = 10,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        HttpRequestSendError = 11,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        HttpRequestParseError = 12,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CallbackNotRegistered = 13,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NetModuleNotInit = 14,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidConfig = 15,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotCreateRuntime = 16,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidContextHandle = 17,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotSerializeResult = 18,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotSerializeError = 19,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotConvertJsValueToJson = 20,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotReceiveSpawnedResult = 21,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SetTimerError = 22,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidParams = 23,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ContractsAddressConversionFailed = 24,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        UnknownFunction = 25,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AppRequestError = 26,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NoSuchRequest = 27,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotSendRequestResult = 28,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotReceiveRequestResult = 29,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotParseRequestResult = 30,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        UnexpectedCallbackResponse = 31,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CanNotParseNumber = 32,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InternalError = 33,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidHandle = 34,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        LocalStorageError = 35,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidData = 36
    }
}