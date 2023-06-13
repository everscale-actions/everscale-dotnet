// See https://aka.ms/new-console-template for more information

using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using Microsoft.Extensions.Options;

var options = new OptionsWrapper<EverClientOptions>(new EverClientOptions());
await using var adapter = new EverClientRustAdapter(options);
var client = new EverClient(adapter);

string phrase = (await client.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom())).Phrase;

Console.WriteLine($"Secret phrase: {phrase}");
