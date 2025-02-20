namespace EverscaleNet.Benchmark;

[SimpleJob(RunStrategy.ColdStart, iterationCount: 1000)]
[MinColumn]
[MaxColumn]
[MeanColumn]
[MedianColumn]
[Outliers(OutlierMode.DontRemove)]
public class EverClientBenchmark {
	private readonly List<string> _englishPhrases = [];
	private readonly List<string> _tonPhrases = [];

	private EverClientRustAdapter _adapter = null!;
	private EverClient _everClient = null!;
	private ParamsOfMnemonicFromRandom _paramsOfMnemonicFromRandomEnglish = null!;
	private ParamsOfMnemonicFromRandom _paramsOfMnemonicFromRandomTon = null!;

	[GlobalSetup]
	public void GlobalSetup() {
		_adapter = new EverClientRustAdapter(new OptionsWrapper<EverClientOptions>(new EverClientOptions()));
		_everClient = new EverClient(_adapter);
		_paramsOfMnemonicFromRandomTon = new ParamsOfMnemonicFromRandom { Dictionary = MnemonicDictionary.Ton };
		_paramsOfMnemonicFromRandomEnglish = new ParamsOfMnemonicFromRandom { Dictionary = MnemonicDictionary.English };
	}

	[GlobalCleanup]
	public async Task GlobalCleanup() {
		await _adapter.DisposeAsync();
	}

	[Benchmark(OperationsPerInvoke = 1000)]
	public async Task Crypto_MnemonicFromRandom_Ton() {
		ResultOfMnemonicFromRandom mnemonicFromRandom = await _everClient.Crypto.MnemonicFromRandom(_paramsOfMnemonicFromRandomTon);
		_tonPhrases.Add(mnemonicFromRandom.Phrase);
	}

	[Benchmark(OperationsPerInvoke = 1)]
	public async Task Crypto_MnemonicVerify_Ton() {
		foreach (string phrase in _tonPhrases) {
			await _everClient.Crypto.MnemonicVerify(new ParamsOfMnemonicVerify { Phrase = phrase, Dictionary = MnemonicDictionary.Ton });
		}
	}

	[Benchmark(OperationsPerInvoke = 1000)]
	public async Task Crypto_MnemonicFromRandom_English() {
		ResultOfMnemonicFromRandom mnemonicFromRandom = await _everClient.Crypto.MnemonicFromRandom(_paramsOfMnemonicFromRandomEnglish);
		_englishPhrases.Add(mnemonicFromRandom.Phrase);
	}

	[Benchmark(OperationsPerInvoke = 1)]
	public async Task Crypto_MnemonicVerify_English() {
		foreach (string phrase in _englishPhrases) {
			await _everClient.Crypto.MnemonicVerify(new ParamsOfMnemonicVerify { Phrase = phrase, Dictionary = MnemonicDictionary.English });
		}
	}
}
