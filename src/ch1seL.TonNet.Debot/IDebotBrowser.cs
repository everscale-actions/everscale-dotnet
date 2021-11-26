using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Debot.Models;

namespace ch1seL.TonNet.Debot;

public interface IDebotBrowser {
	Task Execute(string address, KeyPair keys, Queue<DebotStep> steps);
}