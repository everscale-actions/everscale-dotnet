using System.Collections.Generic;
using System.Threading.Tasks;
using EverscaleNet.Client.Models;
using EverscaleNet.Debot.Models;

namespace EverscaleNet.Debot;

/// <summary>
/// 
/// </summary>
public interface IDebotBrowser {
	/// <summary>
	/// 
	/// </summary>
	/// <param name="address"></param>
	/// <param name="keys"></param>
	/// <param name="steps"></param>
	/// <returns></returns>
	Task Execute(string address, KeyPair keys, Queue<DebotStep> steps);
}
