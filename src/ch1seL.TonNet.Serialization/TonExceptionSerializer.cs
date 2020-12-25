using System;
using System.Collections.Generic;
using System.Text.Json;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Serialization
{
    public static class TonExceptionSerializer
    {
        public static TonClientException GetTonClientExceptionByResponse(JsonElement error)
        {
            ClientError clientError = null;
            Exception innerException = null;
            try
            {
                clientError = error.ToObject<ClientError>();
            }
            catch (Exception e)
            {
                innerException = e;
            }

            return CreateException(clientError, innerException, error.GetRawText);
        }

        public static TonClientException GetTonClientExceptionByResponse(string responseJson)
        {
            ClientError clientError = null;
            Exception innerException = null;
            try
            {
                clientError = JsonSerializer.Deserialize<ClientError>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
            }
            catch (Exception e)
            {
                innerException = e;
            }

            return CreateException(clientError, innerException, () => responseJson);
        }

        private static TonClientException CreateException(ClientError clientError, Exception innerException, Func<string> errorRawTextFunc)
        {
            return clientError == null
                ? new TonClientException($"Raw result: {errorRawTextFunc()}",
                    innerException ?? new NullReferenceException("Result of error response is null or not valid"))
                : TonClientException.CreateExceptionWithCodeWithData(clientError.Code, clientError.Data.ToObject<Dictionary<string, object>>(),
                    clientError.Message);
        }
    }
}