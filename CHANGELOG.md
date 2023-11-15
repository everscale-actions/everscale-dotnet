# Release Notes

All notable changes to this project will be documented in this file.

## [1.28.0.2] – 2022-01-22

### BREAKING CHANGES

- Packages and namespaces renaming ch1seL.TonNet.* to EverscaleNet.*
- IServiceCollection.AddTonClient renamed to IServiceCollection.AddEverClient
- ITonClient renamed to IEverClient
- TonClientException renamed to EverClientException

### Blazor WASM Client

Just use package `EverscaleNet.WebClient` instead of `EverscaleNet.Client` and put your contracts to `wwwroot/_contracts`.

```shell
dotnet add package EverscaleNet.WebClient
```

Enjoy!
