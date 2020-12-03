# Developer guides

## Update TON SDK Version

- Update TON SDK version in [SDKVersion.targets](https://github.com/ton-actions/ton-client-dotnet/blob/master/SDKVersion.targets)
- Generate new modules and models `dotnet run --project tools/ClientGenerator`
- Run all tests and add new if needed
- Commit, push and release new package version
