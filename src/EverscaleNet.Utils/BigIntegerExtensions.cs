using System;
using System.Numerics;

namespace EverscaleNet.Utils;

public static class BigIntegerExtensions {
	public static decimal ToDecimalBalance(this BigInteger bigInteger) {
		return Math.Round((decimal)BigInteger.Divide(bigInteger, 1_000_000) / 1000, 2);
	}
}
