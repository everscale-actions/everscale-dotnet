using System;
using System.Collections.Generic;

namespace EverscaleNet.Models;

/// <summary>
///     Provide code and error message
/// </summary>
public class EverClientException : Exception {
	/// <inheritdoc />
	public EverClientException(string? message = null, Exception? inner = null) : base(message, inner) { }

	/// <summary>
	/// </summary>
	public uint Code { get; private set; }

	/// <summary>
	///     Create exception with code, data and message
	/// </summary>
	/// <param name="code"></param>
	/// <param name="data"></param>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	/// <returns></returns>
	public static EverClientException CreateExceptionWithCodeWithData(uint code, IDictionary<string, object>? data = null, string? message = null,
	                                                                  Exception? inner = null) {
		var exception = new EverClientException(message, inner) { Code = code };
		if (data == null) {
			return exception;
		}
		foreach ((string key, object value) in data) {
			exception.Data.Add(key, value);
		}
		return exception;
	}
}
