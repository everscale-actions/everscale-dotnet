namespace EverscaleNet.Models;

/// <summary>
/// Message type
/// </summary>
public enum MessageType {
	/// <summary>
	/// Internal message
	/// </summary>
	Internal = 0,

	/// <summary>
	/// Inbound message
	/// </summary>
	ExtIn = 1,

	/// <summary>
	/// Outbound message
	/// </summary>
	ExtOut = 2
}
