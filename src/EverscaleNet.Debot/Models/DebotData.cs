using EverscaleNet.Client.Models;

namespace EverscaleNet.Debot.Models;

public class DebotData {
	public string DebotAddr { get; set; }
	public string TargetAddr { get; set; }
	public KeyPair Keys { get; set; }
	public string Abi { get; set; }
}