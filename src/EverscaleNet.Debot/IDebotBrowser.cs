using System.Collections.Generic;
using System.Threading.Tasks;
using EverscaleNet.Client.Models;
using EverscaleNet.Debot.Models;

namespace EverscaleNet.Debot;

public interface IDebotBrowser {
	Task Execute(string address, KeyPair keys, Queue<DebotStep> steps);
}