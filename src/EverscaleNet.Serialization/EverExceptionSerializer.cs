using System;
using System.Collections.Generic;
using System.Text.Json;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;

namespace EverscaleNet.Serialization;

public static class EverExceptionSerializer {
	public static EverClientException GetEverClientExceptionByResponse(JsonElement error) {
		ClientError clientError = null;
		Exception innerException = null;
		try {
			clientError = error.ToObject<ClientError>();
		} catch (Exception e) {
			innerException = e;
		}

		return CreateException(clientError, innerException, error.GetRawText);
	}

	public static EverClientException GetEverClientExceptionByResponse(string responseJson) {
		ClientError clientError = null;
		Exception innerException = null;
		try {
			clientError = JsonSerializer.Deserialize<ClientError>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
		} catch (Exception e) {
			innerException = e;
		}

		return CreateException(clientError, innerException, () => responseJson);
	}

	private static EverClientException CreateException(ClientError clientError, Exception innerException, Func<string> errorRawTextFunc) {
		return clientError == null
			       ? new EverClientException($"Raw result: {errorRawTextFunc()}",
			                                 innerException ?? new NullReferenceException("Result of error response is null or not valid"))
			       : EverClientException.CreateExceptionWithCodeWithData(clientError.Code, clientError.Data.ToObject<Dictionary<string, object>>(),
			                                                             clientError.Message);
	}
}