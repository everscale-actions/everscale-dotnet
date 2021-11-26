# Sample worker services for .NET FreeTON Client

Used contacts:

- https://github.com/tonlabs/samples/blob/master/solidity/15_MessageReceiver.sol
- https://github.com/tonlabs/samples/blob/master/solidity/15_MessageSender.sol

MessageReceiverService:

- deploy 15_MessageReceiver contract
- check count of received messages and log it out to console
- repeat this cycle every 10 seconds

MessageSenderService:

- deploy 15_MessageSender contract
- try send message to 15_MessageReceiver contract
- check count of sent messages and log it out to console
- repeat this cycle every 10 seconds

## Preparing

Download and install .NET 5.0 SDK https://dotnet.microsoft.com/download

### Install and start local Node SE

```
npm install -g ton-dev-cli
ton start
```

## Run MessageReceiverService

```
cd MessageReceiverService
dotnet run
```

## Run MessageSenderService

```
cd MessageSenderService
dotnet run
```