# Developer guides

## Update TON SDK Version

- Update TON SDK version
  in [SDKVersion.targets](https://github.com/everscale-actions/everscale-dotnet/blob/master/SDKVersion.targets)
- Generate new modules and models `dotnet run --project tools/ClientGenerator`
- Run all tests and add new if needed
- Commit, push and release new package version

## Building Nuget package locally

```shell
dotnet pack -o pack -p PackageVersionPostfix=".1-$(date +%s)"
```