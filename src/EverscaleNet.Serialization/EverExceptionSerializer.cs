using System;
using System.Collections.Generic;
using System.Text.Json;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;

namespace EverscaleNet.Serialization;

/// <summary>
///     Deserialize EverClient errors to EverClientException
/// </summary>
public static class EverExceptionSerializer {
	/// <summary>
	///     Deserialize response as ClientError and return EverClientException
	/// </summary>
	/// <param name="responseJson">Raw response</param>
	/// <returns>EverClientException</returns>
	public static EverClientException GetEverClientExceptionByResponse(string responseJson) {
		ClientError clientError = null;
		Exception innerException = null;
		try {
			clientError = JsonSerializer.Deserialize<ClientError>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
		} catch (Exception e) {
			innerException = e;
		}

		return clientError != null
			       ? EverClientException.CreateExceptionWithCodeWithData(clientError.Code, clientError.Data.ToObject<Dictionary<string, object>>(),
			                                                             clientError.Message)
			       : new EverClientException($"Raw result: {responseJson}",
			                                 innerException ?? new NullReferenceException("Result of error response is null or not valid"));
	}
}
