using System.Text.Json;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Exceptions;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet;

/// <summary>
///     Base class for accounts
/// </summary>
public abstract class AccountBase {
	private readonly IEverClient _client;
	private readonly IMultisigAccount? _multisig;
	private readonly IEverPackageManager? _packageManager;
	private Abi? _abi;
	private string? _address;
	private JsonElement? _initialData;
	private KeyPair? _keyPair;
	private string? _tvc;

	/// <summary>
	///     Init account without Signer
	/// </summary>
	/// <param name="client"></param>
	/// <param name="package"></param>
	/// <param name="multisig"></param>
	/// <param name="address">Put or call InitAddress method</param>
	protected AccountBase(IEverClient client, IPackage package, IMultisigAccount? multisig = null, string? address = null) {
		_client = client;
		_multisig = multisig;
		_abi = package.Abi;
		_tvc = package.Tvc;
		_keyPair = package.KeyPair;
		_address = address;
	}

	/// <summary>
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	/// <param name="keyPair"></param>
	/// <param name="multisig"></param>
	/// <param name="address">Put or call InitAddress method</param>
	protected AccountBase(IEverClient client, IEverPackageManager packageManager, KeyPair? keyPair = null, IMultisigAccount? multisig = null, string? address = null) {
		_client = client;
		_packageManager = packageManager;
		_keyPair = keyPair;
		_multisig = multisig;
		_address = address;
	}

	/// <summary>
	/// </summary>
	protected abstract string Name { get; }

	/// <summary>
	///     Account address should be init by .ctor or by InitAddress method
	/// </summary>
	/// <exception cref="AccountNotInitializedException"></exception>
	public string Address => _address ?? throw new AccountNotInitializedException();

	/// <summary>
	///     Init by initialData
	/// </summary>
	/// <param name="publicKey"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	public async Task Init(string? publicKey = null, object? initialData = null, CancellationToken cancellationToken = default) {
		_initialData ??= initialData?.ToJsonElement();
		_address ??= await CalculateAddress(publicKey, cancellationToken);
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
	public async Task<AccountType?> GetAccountType(CancellationToken cancellationToken = default) {
		ResultOfQueryCollection result = await _client.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = Address } }.ToJsonElement(),
			Result = "acc_type",
			Limit = 1
		}, cancellationToken);
		return result.Result.Length == 1
			       ? result.Result[0].Get<AccountType>("acc_type")
			       : null;
	}

	/// <summary>
	///     Call constructor function of contract
	/// </summary>
	/// <param name="parameters"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>deployment fee</returns>
	protected async Task<ResultOfProcessMessage> Deploy(object? parameters = null, CancellationToken cancellationToken = default) {
		CallSet GetCallSet() {
			return new CallSet { FunctionName = "constructor", Input = parameters?.ToJsonElement() };
		}

		KeyPair? keyPair = await GetKeyPair(cancellationToken);
		if (keyPair is not null) {
			return await DeployBySigner(new Signer.Keys { KeysAccessor = keyPair }, GetCallSet(), cancellationToken);
		}
		if (_multisig is not null) {
			return await DeployByMultisig(_multisig, GetCallSet(), cancellationToken);
		}
		throw new CallNotAllowedException("Deploy not allowed because KeyPair or Multisig must set to sign message");
	}

	private async Task<ResultOfProcessMessage> DeployBySigner(Signer signer, CallSet callSet, CancellationToken cancellationToken) {
		if (signer is null) {
			throw new ArgumentNullException(nameof(signer));
		}
		if (callSet is null) {
			throw new ArgumentNullException(nameof(callSet));
		}
		var paramsOfProcessMessage = new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Address = Address,
				Abi = await GetAbi(cancellationToken),
				DeploySet = new DeploySet {
					Tvc = await GetTvc(cancellationToken),
					InitialData = _initialData
				},
				CallSet = callSet,
				Signer = signer
			}
		};
		return await _client.Processing.ProcessMessage(paramsOfProcessMessage, cancellationToken: cancellationToken);
	}

	private async Task<ResultOfProcessMessage> DeployByMultisig(IMultisigAccount multisig, CallSet callSet, CancellationToken cancellationToken) {
		if (multisig is null) {
			throw new ArgumentNullException(nameof(multisig));
		}
		if (callSet is null) {
			throw new ArgumentNullException(nameof(callSet));
		}
		ResultOfEncodeInitialData resultOfEncodeInitialData = await _client.Abi.EncodeInitialData(new ParamsOfEncodeInitialData {
			Abi = await GetAbi(cancellationToken),
			InitialData = _initialData
		}, cancellationToken);

		ResultOfDecodeTvc resultOfDecodeTvc = await _client.Boc.DecodeTvc(new ParamsOfDecodeTvc {
			Tvc = await GetTvc(cancellationToken)
		}, cancellationToken);

		ResultOfEncodeTvc resultOfEncodeTvc = await _client.Boc.EncodeTvc(new ParamsOfEncodeTvc {
			Code = resultOfDecodeTvc.Code,
			Data = resultOfEncodeInitialData.Data
		}, cancellationToken);

		ResultOfEncodeMessageBody resultOfEncodeMessageBody = await _client.Abi.EncodeMessageBody(new ParamsOfEncodeMessageBody {
			Abi = await GetAbi(cancellationToken),
			CallSet = callSet,
			IsInternal = true,
			Signer = new Signer.None()
		}, cancellationToken);

		string payload = resultOfEncodeMessageBody.Body;
		string stateInit = resultOfEncodeTvc.Tvc;
		return await multisig.SubmitTransaction(Address, 5M, false, false, payload, stateInit, cancellationToken);
	}

	/// <summary>
	///     Process message on network and returns detailed information about results including produced transaction and messages.
	/// </summary>
	/// <param name="callSet"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected async Task<ResultOfProcessMessage> Run(CallSet callSet, CancellationToken cancellationToken = default) {
		KeyPair? keyPair = await GetKeyPair(cancellationToken);
		if (keyPair is not null) {
			return await RunBySigner(new Signer.Keys { KeysAccessor = keyPair }, callSet, cancellationToken);
		}
		if (_multisig is not null) {
			return await RunByMultisig(_multisig, callSet, cancellationToken);
		}
		throw new CallNotAllowedException("Call not allowed because KeyPair or Multisig was not set in .ctor");
	}

	private async Task<ResultOfProcessMessage> RunBySigner(Signer signer, CallSet callSet, CancellationToken cancellationToken) {
		if (signer is null) {
			throw new ArgumentNullException(nameof(signer));
		}
		if (callSet is null) {
			throw new ArgumentNullException(nameof(callSet));
		}
		return await _client.Processing.ProcessMessage(new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Address = Address,
				Abi = await GetAbi(cancellationToken),
				CallSet = callSet,
				Signer = signer
			}
		}, cancellationToken: cancellationToken);
	}

	private async Task<ResultOfProcessMessage> RunByMultisig(IMultisigAccount multisig, CallSet callSet, CancellationToken cancellationToken) {
		if (multisig is null) {
			throw new ArgumentNullException(nameof(multisig));
		}
		if (callSet is null) {
			throw new ArgumentNullException(nameof(callSet));
		}
		ResultOfEncodeMessageBody resultOfEncodeMessageBody = await _client.Abi.EncodeMessageBody(new ParamsOfEncodeMessageBody {
			Abi = await GetAbi(cancellationToken),
			CallSet = callSet,
			IsInternal = true,
			Signer = new Signer.None()
		}, cancellationToken);

		string payload = resultOfEncodeMessageBody.Body;
		ResultOfProcessMessage resultOfProcessMessage = await multisig.SendTransaction(Address, 5M, true, 1, payload, cancellationToken);
		if (resultOfProcessMessage.OutMessages.Length == 0) {
			throw new NoOutMessagesException(resultOfProcessMessage);
		}
		string outMessage = resultOfProcessMessage.OutMessages[0];
		ResultOfParse parseResult = await _client.Boc.ParseMessage(new ParamsOfParse { Boc = outMessage }, cancellationToken);
		var outMsg = parseResult.Parsed!.ToPrototype(new { id = default(string) });
		ResultOfQueryCollection resultOfQueryCollection = await _client.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "messages",
			Filter = new { id = new { eq = outMsg.id } }.ToJsonElement(),
			Result = "dst_transaction{aborted compute{success exit_code}}"
		}, cancellationToken);
		var result = resultOfQueryCollection.Result[0].ToPrototype(new {
			dst_transaction = new {
				aborted = default(bool),
				compute = new {
					success = default(bool),
					exit_code = default(uint)
				}
			}
		});
		if (result.dst_transaction.aborted || !result.dst_transaction.compute.success) {
			throw EverClientException.CreateExceptionWithCodeWithData(result.dst_transaction.compute.exit_code, resultOfProcessMessage.ToJsonElement().ToObject<Dictionary<string, object>>(),
			                                                          "Transaction aborted or failed");
		}
		return resultOfProcessMessage;
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
