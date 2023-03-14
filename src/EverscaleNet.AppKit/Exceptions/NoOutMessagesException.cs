using System;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Exceptions;

/// <summary>
/// Throws if no out messages created by multisig payload
/// </summary>
public class NoOutMessagesException : Exception {
	/// <summary>
	/// 
	/// </summary>
	/// <param name="resultOfProcessMessage"></param>
	public NoOutMessagesException(ResultOfProcessMessage resultOfProcessMessage) {
		base.Data["resultOfProcessMessage"] = resultOfProcessMessage;
	}
}