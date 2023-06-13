# Sample worker services for Everscale network

### Used contacts:

- https://github.com/tonlabs/samples/blob/master/solidity/1_Accumulator.sol
- https://github.com/tonlabs/samples/blob/master/solidity/15_MessageReceiver.sol
- https://github.com/tonlabs/samples/blob/master/solidity/15_MessageSender.sol
- https://github.com/tonlabs/samples/blob/master/solidity/20_bomber.sol
- https://github.com/tonlabs/samples/blob/master/solidity/20_sink.sol

## Preparing

Download and install latest .NET SDK https://dotnet.microsoft.com/download

### Start local Node SE

```
npx -y everdev se start
```

## ConsoleApp

A simple console app that just generates a random seed phrase

```shell
dotnet run --project ConsoleApp
```

## Blockchain message sending and receiving

### MessageReceiverService

- deploy 15_MessageReceiver contract
- check count of received messages and log it out to console
- repeat this cycle every 10 seconds

```shell
dotnet run --project MessageReceiverService
```

### MessageSenderService

- deploy 15_MessageSender contract
- try send message to 15_MessageReceiver contract
- check count of sent messages and log it out to console
- repeat this cycle every 10 seconds

```shell
dotnet run --project MessageSenderService
```

## TestingExample

Example of a contract testing project with XUnit framework

```shell
dotnet test TestingExample
```

## BlazorApp

WASM app demo

```shell
dotnet run --project BlazorApp
```
