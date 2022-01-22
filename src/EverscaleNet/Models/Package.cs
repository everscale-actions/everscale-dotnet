using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

public class Package {
	public Package(Abi abi, string tvc) {
		Abi = abi;
		Tvc = tvc;
	}

	public Abi Abi { get; }
	public string Tvc { get; }
}