namespace EverscaleNet;

/// <summary>
///     Base class for accounts
/// </summary>
public abstract class AccountBase : IAccount {
	private readonly IEverClient _client;
	private readonly IEverPackageManager? _packageManager;
	private Abi? _abi;
	private string? _address;
	private JsonElement? _initialData;
	private IInternalSender? _internalSender;
	private KeyPair? _keyPair;
	private string? _tvc;

	/// <summary>
	///     Init account without Signer
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	/// <param name="address">Put or call InitAddress method</param>
	protected AccountBase(IEverClient client, IEverPackageManager packageManager, string? address = null) {
		_client = client;
		_packageManager = packageManager;
		_address = address;
	}

	/// <summary>
	///     Init account without Signer
	/// </summary>
	/// <param name="client"></param>
	/// <param name="package">IPackage with abi, tv and keypair</param>
	/// <param name="address">Put or call InitAddress method</param>
	protected AccountBase(IEverClient client, IPackage package, string? address = null) {
		_client = client;
		_abi = package.Abi;
		_tvc = package.Tvc;
		_keyPair = package.KeyPair;
		_address = address;
	}

	/// <summary>
	/// </summary>
	protected virtual string Name => GetType().Name;

	/// <summary>
	///     Account address should be init by .ctor or by InitAddress method
	/// </summary>
	/// <exception cref="AccountNotInitializedException"></exception>
	public string Address => _address ?? throw new AccountNotInitializedException();

	/// <summary>
	///     Init by publicKey and initialData
	/// </summary>
	/// <param name="publicKey"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	public async Task Init(string? publicKey = null, object? initialData = null, CancellationToken cancellationToken = default) {
		_initialData ??= initialData?.ToJsonElement();
		_address ??= await CalculateAddress(publicKey, cancellationToken);
	}

	/// <summary>
	///     Init with keyPair and initialData
	/// </summary>
	/// <param name="keyPair"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	public async Task Init(KeyPair keyPair, object? initialData = null, CancellationToken cancellationToken = default) {
		_keyPair ??= keyPair;
		_initialData ??= initialData?.ToJsonElement();
		_address ??= await CalculateAddress(keyPair.Public, cancellationToken);
	}

	/// <summary>
	///     Init with Multisig Account and init data
	/// </summary>
	/// <param name="internalSender"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	public async Task Init(IInternalSender internalSender, object? initialData = null, CancellationToken cancellationToken = default) {
		_internalSender ??= internalSender;
		_initialData ??= initialData?.ToJsonElement();
		_address ??= await CalculateAddress(null, cancellationToken);
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="AccountDoesNotExistException"></exception>
	public async Task<decimal> GetBalance(CancellationToken cancellationToken = default) {
		ResultOfQueryCollection result = await _client.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = Address } }.ToJsonElement(),
			Result = "balance(format: DEC)",
			Limit = 1
		}, cancellationToken);

		if (result.Result.Length == 0) {
			throw new AccountDoesNotExistException();
		}
		return result.Result[0].Get<string>("balance").NanoToCoins();
	}

	/// <summary>
	///     Try to find contract in blockchain
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>Returns true if the contract exists</returns>
	public async Task<AccountType> GetAccountType(CancellationToken cancellationToken = default) {
		ResultOfQueryCollection result = await _client.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = Address } }.ToJsonElement(),
			Result = "acc_type",
			Limit = 1
		}, cancellationToken);
		return result.Result.Length == 1
			       ? result.Result[0].Get<AccountType>("acc_type")
			       : AccountType.NonExist;
	}

	/// <summary>
	///     Call constructor function of contract
	/// </summary>
	/// <param name="parameters"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>deployment fee</returns>
	protected Task<ResultOfProcessMessage> Deploy(object? parameters = null, CancellationToken cancellationToken = default) {
		return Run("constructor", parameters, cancellationToken);
	}

	/// <summary>
	///     Process message on network and returns detailed information about results including produced transaction and messages.
	/// </summary>
	/// <param name="functionName"></param>
	/// <param name="parameters"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected async Task<ResultOfProcessMessage> Run(string functionName, object? parameters, CancellationToken cancellationToken = default) {
		if (functionName is not "constructor"
		    && await GetAccountType(cancellationToken) is not AccountType.Active) {
			throw new AccountIsNotActiveException();
		}
		Func<CallSet, Task<ResultOfProcessMessage>> run;
		if (_internalSender is not null) {
			run = callSet => RunByInternalSender(_internalSender, callSet, cancellationToken);
		} else {
			KeyPair? keyPair = await GetKeyPair(cancellationToken);
			if (keyPair is not null) {
				run = callSet => RunBySigner(new Signer.Keys { KeysAccessor = keyPair }, callSet, cancellationToken);
			} else {
				throw new CallNotAllowedException("Call not allowed because KeyPair or IInternalSender was not set in .ctor");
			}
		}
		var callSet = new CallSet {
			FunctionName = functionName,
			Input = parameters?.ToJsonElement()
		};
		return await run(callSet);
	}

	private async Task<ResultOfProcessMessage> RunByInternalSender(IInternalSender sender, CallSet callSet, CancellationToken cancellationToken) {
		Abi abi = await GetAbi(cancellationToken);

		string? stateInit = callSet.FunctionName == "constructor" ? await GetStateInit(cancellationToken) : null;

		return await sender.Send(Address, 10m, true, false, abi, callSet, stateInit, cancellationToken);
	}

	private async Task<ResultOfProcessMessage> RunBySigner(Signer signer, CallSet callSet, CancellationToken cancellationToken) {
		DeploySet? deploySet = callSet.FunctionName is "constructor"
			                       ? new DeploySet {
				                       Tvc = await GetTvc(cancellationToken),
				                       InitialData = _initialData
			                       }
			                       : null;
		var paramsOfProcessMessage = new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Address = Address,
				Abi = await GetAbi(cancellationToken),
				DeploySet = deploySet,
				CallSet = callSet,
				Signer = signer
			}
		};
		return await _client.Processing.ProcessMessage(paramsOfProcessMessage, cancellationToken: cancellationToken);
	}

	private async Task<string> GetStateInit(CancellationToken cancellationToken) {
		ResultOfEncodeInitialData resultOfEncodeInitialData = await _client.Abi.EncodeInitialData(new ParamsOfEncodeInitialData {
			Abi = await GetAbi(cancellationToken),
			InitialData = _initialData
		}, cancellationToken);
		ResultOfDecodeStateInit resultOfDecodeStateInit = await _client.Boc.DecodeStateInit(new ParamsOfDecodeStateInit {
			StateInit = await GetTvc(cancellationToken)
		}, cancellationToken);
		ResultOfEncodeStateInit resultOfEncodeTvc = await _client.Boc.EncodeStateInit(new ParamsOfEncodeStateInit {
			Code = resultOfDecodeStateInit.Code,
			Data = resultOfEncodeInitialData.Data
		}, cancellationToken);
		return resultOfEncodeTvc.StateInit;
	}

	/// <summary>
	///     Get tvc of contract
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>tvc string of code</returns>
	protected async Task<string> GetTvc(CancellationToken cancellationToken) {
		return (_tvc ??= await _packageManager!.LoadTvc(Name, cancellationToken)) ?? throw new InvalidOperationException();
	}

	/// <summary>
	///     Get contract Abi
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected async Task<Abi> GetAbi(CancellationToken cancellationToken) {
		return (_abi ??= await _packageManager!.LoadAbi(Name, cancellationToken)) ?? throw new InvalidOperationException();
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	protected async Task<KeyPair?> GetKeyPair(CancellationToken cancellationToken) {
		return (_keyPair ??= await _packageManager!.LoadKeyPair(Name, cancellationToken)) ?? null;
	}

	private async Task<string> CalculateAddress(string? publicKey, CancellationToken cancellationToken) {
		var paramsOfEncodeMessage = new ParamsOfEncodeMessage {
			Abi = await GetAbi(cancellationToken),
			DeploySet = new DeploySet {
				Tvc = await GetTvc(cancellationToken),
				InitialData = _initialData
			},
			Signer = publicKey is null ? new Signer.None() : new Signer.External { PublicKey = publicKey }
		};
		ResultOfEncodeMessage resultOfEncodeMessage = await _client.Abi.EncodeMessage(paramsOfEncodeMessage, cancellationToken);
		return resultOfEncodeMessage.Address;
	}
}
