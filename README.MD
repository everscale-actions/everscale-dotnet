![Main workflow](https://github.com/ton-actions/ton-client-dotnet/workflows/Main%20workflow/badge.svg)

# Free TON .NET Client 

Free TON is modern and fast crypto network. Lets do this network convenient both for users and developers!

This client was automatically generated from api.json https://github.com/tonlabs/TON-SDK/blob/master/tools/api.json and fully support method provided in SDK documentation https://github.com/tonlabs/TON-SDK/tree/master/docs

# Quick start 

## Add Nuget Package to your project

```
dotnet add ch1seL.TonNet.RustClient
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
        var mnemonic = await _tonClient.Crypto.MnemonicFromRandom(new MnemonicFromRandomRequest());
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

## or you configure options by appsettings.json or another configuration provider

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




