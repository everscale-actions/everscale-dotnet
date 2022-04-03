![Main workflow](https://github.com/everscale-actions/everscale-dotnet/workflows/Main%20workflow/badge.svg)
[![Chat on Telegram](https://img.shields.io/badge/chat-on%20telegram-9cf.svg)](https://t.me/everscale_actions_chat)
[<img src="https://avatars3.githubusercontent.com/u/67861283?s=150&u=4536b61595a1b422604fab8a7012092d891278f6&v=4" align="right" width="150">](https://freeton.org/)

# Everscale .NET Client 


Everscale is secure and scalable network. Lets do this network convenient both for users and developers!

- This client was automatically generated from [api.json](https://github.com/tonlabs/TON-SDK/blob/master/tools/api.json) (see [ClientGenerator](https://github.com/everscale-actions/everscale-dotnet/tree/master/tools/EverscaleNet.ClientGenerator)) 
- Fully supported methods provided in SDK documentation https://github.com/tonlabs/TON-SDK/tree/master/docs
- No Newtonsoft.Json required, it is kinda legacy now, last release was over a year ago. New System.Text.Json is ten times faster
- The most complete support of CancellationToken
- Net Standard 2.1 compatible


# Quick start 

## Add Nuget Package to your project

```shell
# Typical .Net application
dotnet add package EverscaleNet.Client

# Blazor WASM application
dotnet add package EverscaleNet.WebClient
```

## Register in DI  
**Be careful!** no network endpoints provided as default

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddEverClient();
}
```

## Ready to use everywhere 

```
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

## IPackageManager interface

There is easy option to load contracts abi and tvm info from files in this client.

Now available following async methods:

```
Task<Package> LoadPackage(string name); // Package entity just contains Abi and Tvc
Task<Abi> LoadAbi(string name);
Task<string> LoadTvc(string name);
```

Default contracts path is `_contracts`. **Be careful**, Blazor WASM app will search for `_contracts` relative to `wwwroot`. 

## Advanced client configuration

See configuration parameters:

* https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#network-config
* https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#crypto-config
* https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#abi-config

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddEverClient(config =>
    {
        config.Network.Endpoints = new[] { // see avalable networks https://tonlabs.gitbook.io/ton-sdk/reference/ton-os-api/networks#networks // };
        config.Network.NetworkRetriesCount = 5;
    }, packageManagerConfig =>
    {
        packageManagerConfig.PackagesPath = "packages"; // path to abi.json and tvc files, _contracts is default
    });  
}
```

### or configure options by appsettings.json or another configuration provider

https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers

#### Example for appsettings.json

```
{
  "EverClient": {
    "Network": {
      "Endpoints": [ "https://eri01.net.everos.dev/", "https://rbx01.net.everos.dev/", "https://gra01.net.everos.dev/" ],
      "WaitForTimeout": 5000
    }
  },
  "PackageManager": {
    "PackagesPath": "my_app_contracts"
  }
}
```

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddEverClient()        
        .Configure<EverClientOptions>(Configuration.GetSection("EverClient"))
        .Configure<PackageManagerOptions>(Configuration.GetSection("PackageManager"));
}
```

## Logging

Fully compatible with https://docs.microsoft.com/en-us/dotnet/core/extensions/logging 

## Anonymous type extensions

There are a few properties with type JsonElement in data models. 
And this client provide methods to easy convert this properties to/from Anonymous prototype.

### Convert to anonymous type example:

```
ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse
{
    Boc = "te6ccgEBAQEAWAAAq2n+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE/zMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzSsG8DgAAAAAjuOu9NAL7BxYpA"
});
var parsedPrototype = new {type = default(int), id = default(string)};
var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);

_logger.LogInformation("Parsed message id: {id} type: {type}", parsedMessage.id, parsedMessage.type);
```

### Convert from anonymous type example:

```
await everClient.Net.WaitForCollection(new ParamsOfWaitForCollection
{
    Collection = "transactions",
    Filter = new {in_msg = new {eq = parsedMessage.id}}.ToJsonElement(),
    Result = "id"
});
```

## Samples

https://github.com/everscale-actions/everscale-dotnet/tree/master/samples/

## Support us

This project has no funding, but everyone can support.

Surf Wallet: [0:9b487d68e4f029ab6d92640892d99d1c549ae69b198df414e905350559a165bf](https://uri.ever.surf/surf/0:9b487d68e4f029ab6d92640892d99d1c549ae69b198df414e905350559a165bf)
