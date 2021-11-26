using System.Runtime.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models;

public enum ParamGenericName {
	[EnumMember(Value = "Arc")]
	Arc,
	[EnumMember(Value = "AppObject")]
	AppObject
}