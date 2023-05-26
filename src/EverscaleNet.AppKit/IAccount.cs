namespace EverscaleNet;

/// <summary>
/// </summary>
public interface IAccount {
	/// <summary>
	///     Account address should be init by .ctor or by InitAddress method
	/// </summary>
	/// <exception cref="AccountNotInitializedException"></exception>
	string Address { get; }

	/// <summary>
	///     Init by publicKey and initialData
	/// </summary>
	/// <param name="publicKey"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	Task Init(string? publicKey = null, object? initialData = null, CancellationToken cancellationToken = default);

	/// <summary>
	///     Init with keyPair and initialData
	/// </summary>
	/// <param name="keyPair"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	Task Init(KeyPair keyPair, object? initialData = null, CancellationToken cancellationToken = default);

	/// <summary>
	///     Init with Multisig Account and init data
	/// </summary>
	/// <param name="internalSender"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	Task Init(IInternalSender internalSender, object? initialData = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="AccountDoesNotExistException"></exception>
	Task<decimal> GetBalance(CancellationToken cancellationToken = default);

	/// <summary>
	///     Try to find contract in blockchain
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>Returns true if the contract exists</returns>
	Task<AccountType> GetAccountType(CancellationToken cancellationToken = default);
}
