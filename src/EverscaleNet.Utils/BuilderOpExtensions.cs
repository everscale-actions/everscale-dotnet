using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;

namespace EverscaleNet.Utils;

/// <summary>
/// </summary>
public static class BuilderOpExtensions {
	/// <summary>
	///     Create BuilderOp from bool
	/// </summary>
	/// <param name="b">boolean value</param>
	/// <returns>
	///     <see cref="BuilderOp.Integer" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this bool b) {
		return new BuilderOp.Integer { Size = 1, Value = (b ? 1 : 0).ToJsonElement<int>() };
	}

	/// <summary>
	///     Create BuilderOp from byte
	/// </summary>
	/// <param name="b">byte value</param>
	/// <returns>
	///     <see cref="BuilderOp.Integer" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this byte b) {
		return new BuilderOp.Integer { Size = 8, Value = b.ToJsonElement<byte>() };
	}

	/// <summary>
	///     Create BuilderOp from short
	/// </summary>
	/// <param name="s">short value</param>
	/// <returns>
	///     <see cref="BuilderOp.Integer" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this short s) {
		return new BuilderOp.Integer { Size = 16, Value = s.ToJsonElement<short>() };
	}

	/// <summary>
	///     Create BuilderOp from int
	/// </summary>
	/// <param name="i">int value</param>
	/// <returns>
	///     <see cref="BuilderOp.Integer" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this int i) {
		return new BuilderOp.Integer { Size = 32, Value = i.ToJsonElement<int>() };
	}

	/// <summary>
	///     Create BuilderOp from long
	/// </summary>
	/// <param name="l">long value</param>
	/// <returns>
	///     <see cref="BuilderOp.Integer" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this long l) {
		return new BuilderOp.Integer { Size = 64, Value = l.ToJsonElement<long>() };
	}

	/// <summary>
	///     Create BuilderOp from string
	/// </summary>
	/// <param name="s">string value</param>
	/// <returns>
	///     <see cref="BuilderOp.BitString" />
	/// </returns>
	public static BuilderOp ToBuilderOp(this string s) {
		return new BuilderOp.BitString { Value = s };
	}

	/// <summary>
	///     Create BuilderOp.CellBoc from byte
	/// </summary>
	/// <param name="s">string-boc value</param>
	/// <returns>
	///     <see cref="BuilderOp.CellBoc" />
	/// </returns>
	public static BuilderOp ToBuilderOpCellBoc(this string s) {
		return new BuilderOp.CellBoc { Boc = s };
	}
}
