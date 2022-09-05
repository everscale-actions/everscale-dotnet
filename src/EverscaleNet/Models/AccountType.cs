namespace EverscaleNet.Models;

/// <summary>
/// The current status of the account according to original Everscale blockchain specification
/// </summary>
public enum AccountType {
	/// <summary>
	/// Account has balance but no code
	/// </summary>
	Uninit = 0,
	/// <summary>
	/// Account has balance and code
	/// </summary>
	Active = 1,
	/// <summary>
	/// Account has been frozen for some reasons
	/// </summary>
	Frozen = 2,
	/// <summary>
	/// Account has been deleted
	/// </summary>
	NonExist = 3
}
