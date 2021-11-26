using System.Collections.Generic;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Debot.Models;

public class ExpectedTransaction {
	public string Dst { get; set; }
	public List<Spending> Out { get; set; }
	public bool SetCode { get; set; }
	public string SignKey { get; set; }
	public bool Approved { get; set; }
}