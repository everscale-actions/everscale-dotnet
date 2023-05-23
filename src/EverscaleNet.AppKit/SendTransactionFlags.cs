namespace EverscaleNet;

/// <summary>
///     Used for SendTransaction method
/// </summary>
[Flags]
public enum SendTransactionFlags {
	/// <summary>
	///     sender wants to pay transfer fees separately (from account balance instead of message balance)
	/// </summary>
	SenderWantsToPayTransferFeesSeparately = 1,

	/// <summary>
	///     if there are some errors during the action phase it should be ignored (don't fail transaction e.g. if message balance is greater than remaining balance, or
	///     it has invalid address)
	/// </summary>
	IgnoreSomeErrors = 2,

	/// <summary>
	///     current account must be destroyed if its resulting balance is zero (usually combined with flag 128)
	/// </summary>
	DestroyedIfZero = 32,

	/// <summary>
	///     used for messages that carry all the remaining value of the inbound message in addition to the value initially indicated in the new message
	/// </summary>
	CarryAllRemainingValue = 64,

	/// <summary>
	///     message will carry all the remaining balance
	/// </summary>
	CarryAllRemainingBalance = 128
}
