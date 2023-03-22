using System;

namespace EverscaleNet.Exceptions;

/// <summary>
///     Throws if call not allowed for some reason
/// </summary>
public class CallNotAllowedException : Exception {
	/// <summary>
	/// </summary>
	/// <param name="message"></param>
	public CallNotAllowedException(string message) : base(message) { }
}
