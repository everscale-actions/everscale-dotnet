using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Cell builder operation.</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Integer), nameof(Integer))]
    [JsonDerivedType(typeof(BitString), nameof(BitString))]
    [JsonDerivedType(typeof(Cell), nameof(Cell))]
    [JsonDerivedType(typeof(CellBoc), nameof(CellBoc))]
    [JsonDerivedType(typeof(Address), nameof(Address))]
    public abstract class BuilderOp
    {
        /// <summary>
        /// <para>Append integer to cell data.</para>
        /// </summary>
        public class Integer : BuilderOp
        {
            /// <summary>
            /// <para>Bit size of the value.</para>
            /// </summary>
            [JsonPropertyName("size")]
            public uint Size { get; set; }

            /// <summary>
            /// <para>Value: - `Number` containing integer number.</para>
            /// </summary>
            [JsonPropertyName("value")]
            public JsonElement? Value { get; set; }
        }

        /// <summary>
        /// <para>Append bit string to cell data.</para>
        /// </summary>
        public class BitString : BuilderOp
        {
            /// <summary>
            /// <para>Bit string content using bitstring notation. See `TON VM specification` 1.0.</para>
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// <para>Append ref to nested cells.</para>
        /// </summary>
        public class Cell : BuilderOp
        {
            /// <summary>
            /// <para>Nested cell builder.</para>
            /// </summary>
            [JsonPropertyName("builder")]
            public BuilderOp[] Builder { get; set; }
        }

        /// <summary>
        /// <para>Append ref to nested cell.</para>
        /// </summary>
        public class CellBoc : BuilderOp
        {
            /// <summary>
            /// <para>Nested cell BOC encoded with `base64` or BOC cache key.</para>
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }
        }

        /// <summary>
        /// <para>Address.</para>
        /// </summary>
        public class Address : BuilderOp
        {
            /// <summary>
            /// <para>Address in a common `workchain:account` or base64 format.</para>
            /// </summary>
            [JsonPropertyName("address")]
            public string AddressAccessor { get; set; }
        }
    }
}