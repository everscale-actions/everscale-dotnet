![Main workflow](https://github.com/ton-actions/ton-client-dotnet/workflows/Main%20workflow/badge.svg)
[<img src="https://avatars3.githubusercontent.com/u/67861283?s=150&u=4536b61595a1b422604fab8a7012092d891278f6&v=4" align="right" width="150">](https://freeton.org/)

# Free TON .NET Client 

Free TON is modern and fast crypto network. Lets do this network convenient both for users and developers!

- This client was automatically generated from api.json https://github.com/tonlabs/TON-SDK/blob/master/tools/api.json 
- Fully supported methods provided in SDK documentation https://github.com/tonlabs/TON-SDK/tree/master/docs
- No Newtonsoft.Json required, it is kinda legacy now, last release was over a year ago. New System.Text.Json is ten times faster
- The most complete support of CancellationToken
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) (support for previous versions .NET Core 2.x, 3.x will be soon...)


# Quick start 

## Add Nuget Package to your project

```
dotnet add package ch1seL.TonNet.Client
```

## Register in DI  

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTonClient();
}
```

## Ready to use everywhere 

```
public class YourTonService {
    private readonly ITonClient _tonClient;

    public YourTonService(ITonClient tonClient) {
        _tonClient = tonClient;
    }
    
    public string GetTonSecretPhase() {
        var mnemonic = await _tonClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom());
        return mnemonic.Phrase;
    }
}
```

## Advanced client configuration

see configuration parameters here https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#NetworkConfig

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTonClient(config =>
        {
            config.ServerAddress = "net.ton.dev"; // or http://localhost
            config.NetworkRetriesCount = 5;
        });
}
```

## or configure options by appsettings.json or another configuration provider

https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers

### example for appsettings.json 
```
{
  "NetworkConfig": {
    "server_address": "net.ton.dev",
    "network_retries_count": "10",
    "message_retries_count": "5",
    "message_processing_timeout": 100,
    "wait_for_timeout": 100,
    "out_of_sync_threshold": 5000,
    "access_key": "YourAccessKey"
  }
}
```

```
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<NetworkConfig>(Configuration.GetSection("NetworkConfig"));
    services.AddTonClient();
}
```

## Logging

Fully compatible with https://docs.microsoft.com/en-us/dotnet/core/extensions/logging 

## Anonymous type extensions

There are a few properties with type JsonElement in data models. 
This client provide methods to easy convert this properties to Anonymous prototype.

Simple example.:
```
ResultOfParse parseResult = await tonClient.Boc.ParseMessage(new ParamsOfParse
{
    Boc = "te6ccgEBAQEAWAAAq2n+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE/zMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzSsG8DgAAAAAjuOu9NAL7BxYpA"
});
var parsedPrototype = new {type = default(int), id = default(string)};
var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);

_logger.LogInformation("Parsed message id: {id} type: {type}", parsedMessage.id, parsedMessage.type);
```

#### Enjoy!

#### My Coffee Surf address:
ton://surf/0:9b487d68e4f029ab6d92640892d99d1c549ae69b198df414e905350559a165bf

https://ton.surf
