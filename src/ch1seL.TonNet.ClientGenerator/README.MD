# ClientGenerator

TargetFramework: net5.0

This is tool generate Ton Client .NET 

## Using

- Copy api.json to Resources/api.json

- Run: `dotnet run`


## Result

- Modules and Models will placed in ../ch1seL.TonNet.Client.Modules/Generated
  
- TonClient will placed in ../ch1seL.TonNet.Client/Generated will be overriden

## Roadmap 

- Build-in running to generate client during the build

- Download file api.json by version mentioned in .csproj file