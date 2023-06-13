![Main workflow](https://github.com/everscale-actions/everscale-dotnet/workflows/Main%20workflow/badge.svg)
[![Chat on Telegram](https://img.shields.io/badge/chat-on%20telegram-9cf.svg)](https://t.me/everscale_actions_chat)
[<img src="https://avatars3.githubusercontent.com/u/67861283?s=150&u=4536b61595a1b422604fab8a7012092d891278f6&v=4" align="right" width="150">](https://freeton.org/)

# Everscale .NET Client

[Everscale](https://everscale.network/) is secure and scalable network. Lets do this network convenient both for users
and developers!

- This client was automatically generated
  from [api.json](https://github.com/tonlabs/TON-SDK/blob/master/tools/api.json) (
  see [ClientGenerator](https://github.com/everscale-actions/everscale-dotnet/tree/master/tools/EverscaleNet.ClientGenerator))
- Fully supported methods provided in SDK documentation https://github.com/tonlabs/TON-SDK/tree/master/docs
- No Newtonsoft.Json required
- The most complete support of CancellationToken
- Net Standard 2.1, Net 6, Net 7 compatible

# Quick start

**Be careful!** no network endpoints provided as default

### Typical .Net application

```shell
dotnet add package EverscaleNet.Client
```

```csharp
builder.Services.AddEverClient();
```

### Blazor WASM application

```shell
dotnet add package EverscaleNet.WebClient
```

```csharp
builder.Services.AddEverClient();
```

## Ready to use everywhere

```csharp
public class YourEverService {
    private readonly IEverClient _everClient;

    public YourEverService(IEverClient everClient) {
        _everClient = everClient;
    }
    
    public string GetEverSecretPhase() {
        var mnemonic = await _everClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom());
        return mnemonic.Phrase;
    }
}
```

## PackageManager

There is easy option to load contracts abi, tvm, keys or code info from files or web in this client.

Now available following async methods:

```csharp
public interface IEverPackageManager {
    Task<Package> LoadPackage(string name); // Load whole package within Abi, Tvc, KeyPair and Code
    Task<Abi> LoadAbi(string name);         // deserialize abi json to Abi
    Task<string> LoadTvc(string name);      // compiled contract in base64
    Task<KeyPair> LoadKeyPair(string name); // deserialize keypair to KeyPair 
    Task<string> LoadCode(string name);     // Load Code as text
}
```

Default contracts path is `_contracts`.
**Be careful**, Blazor WASM app will search for `_contracts` relative to `wwwroot`.

## Advanced client configuration

See configuration client parameters:

* https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#clientconfig
* https://docs.everos.dev/ever-sdk/guides/configuration/endpoint-configuration

### Example configuration for typical client

```csharp
builder.Services.AddEverClient(client => {
        client.Network.Endpoints = new[] { "http://mainnet.evercloud.dev/your-project-id-here/graphql" };
        client.Network.NetworkRetriesCount = 5;
    }, packageManager =>
    {
        packageManager.PackagesPath = "_my_contracts"; // path to files, _contracts is default
        packageManager.AbiFileTemplate = "{0}.abi.json"; 
        packageManager.TvcFileTemplate = "{0}.tvc";
        packageManager.KeyPairFileTemplate = "{0}.keys.json"; 
        packageManager.CodeFileTemplate = "{0}.code"; 
    });
```

### Example configuration for Blazor Wasm client

```csharp
builder.Services
       .AddEverClient(
	       client => {
		       client.Network.Endpoints = new[] { "http://mainnet.evercloud.dev/your-project-id-here/graphql" };
		       ..
	       }, packageManager => {
		       packageManager.BasePath = "http://your_site.com"; // can be builder.HostEnvironment.BaseAddress
		       packageManager.PackagesPath = "_my_contracts"; // path relative to `wwwroot`
		       ..
	       }, libWeb => { 
		       // configuring js wasm wrapper
		       // see https://github.com/tonlabs/ever-sdk-js#setup-library
		       libWeb.DisableSeparateWorker = false;
		       libWeb.BinaryUrl = "/_content/EverscaleNet.Adapter.Wasm/eversdk.wasm";
	       })
```

Blazor WASM [sample](https://github.com/everscale-actions/everscale-dotnet/tree/main/samples/BlazorApp)

### Configure options by appsettings.json or another configuration provider

https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers

#### Example for appsettings.json

```csharp
{
  "EverClient": {
    "Network": {
      "Endpoints": [ "http://mainnet.evercloud.dev/your-project-id-here/graphql" ],
      "WaitForTimeout": 5000
    }
  },
  "PackageManager": {
    "PackagesPath": "_my_contracts"
  }
}
```

```csharp
builder.Services.AddEverClient()        
        .Configure<EverClientOptions>(Configuration.GetSection("EverClient"))
        .Configure<PackageManagerOptions>(Configuration.GetSection("PackageManager"));
```

## Logging

Fully compatible with https://docs.microsoft.com/en-us/dotnet/core/extensions/logging

## Prototype type extensions

There are a few properties with type JsonElement in data models.
And this client provide methods to easy convert this properties to/from Prototype.

### Convert to Prototype example:

```
ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse
{
    Boc = "te6ccgEBAQEAWAAAq2n+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE/zMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzSsG8DgAAAAAjuOu9NAL7BxYpA"
});
var parsedPrototype = new {type = default(int), id = default(string)};
var parsedMessage = parseResult.Parsed!.Value.ToPrototype(parsedPrototype);

_logger.LogInformation("Parsed message id: {id} type: {type}", parsedMessage.id, parsedMessage.type);
```

### Convert from Prototype example:

```
await everClient.Net.WaitForCollection(new ParamsOfWaitForCollection
{
    Collection = "transactions",
    Filter = new {in_msg = new {eq = parsedMessage.id}}.ToJsonElement(),
    Result = "id"
});
```

## Testing Framework and AppKit

```shell
dotnet add package EverscaleNet.AppKit
dotnet add package EverscaleNet.Testing
```

There are a few useful things in the libs

### EverscaleNet.AppKit - simplify work with contracts and accounts

- AccountBase - used to create C# contact wrappers
- IMultisigAccount(MultisigAccount) - base for mutisig wallet
- automatically compile all sol and tsol files in your project

Solidity compiling parameters(set with PropertyGroup or `-p` parameter):

- SolCompilerVersion (default: latest)
- TVMLinkerVersion (default: latest)
- ContractsBasePath (default: _contracts)

### EverscaleNet.Testing - simplify the testing

- InitKeyPairService - hosted service to add KeyPair with random keys to DI
- IEverGiver(EverGiverV3) - giver interface configured by GiverOptions (SE keys by default)
- InitMultisigAccountService - hosted service to init MultisigAccount

see examples for testing
framework https://github.com/everscale-actions/everscale-dotnet/tree/main/samples/TestingExample

## Samples

https://github.com/everscale-actions/everscale-dotnet/tree/master/samples/

## Support us

This project has no funding, but everyone can support.

Surf
Wallet: [0:9b487d68e4f029ab6d92640892d99d1c549ae69b198df414e905350559a165bf](https://uri.ever.surf/surf/0:9b487d68e4f029ab6d92640892d99d1c549ae69b198df414e905350559a165bf)
