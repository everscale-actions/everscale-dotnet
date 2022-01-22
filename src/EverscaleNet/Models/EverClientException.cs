using System;
using System.Collections.Generic;

namespace EverscaleNet.Models;

public class EverClientException : Exception {
	public EverClientException(string message = null, Exception inner = null) : base(message, inner) { }

	public uint Code { get; private set; }

	public static EverClientException CreateExceptionWithCodeWithData(uint code, IDictionary<string, object> data = null, string message = null,
	                                                                  Exception inner = null) {
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