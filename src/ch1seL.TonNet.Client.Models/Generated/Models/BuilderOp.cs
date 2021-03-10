using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Cell builder operation.
    /// </summary>
    public abstract class BuilderOp
    {
        /// <summary>
        /// Append integer to cell data.
        /// </summary>
        [JsonDiscriminator("Integer")]
        public class Integer : BuilderOp
        {
            /// <summary>
            /// Append integer to cell data.
            /// </summary>
            [JsonPropertyName("size")]
            public byte Size { get; set; }

            /// <summary>
            /// Append integer to cell data.
            /// </summary>
            [JsonPropertyName("value")]
            public JsonElement? Value { get; set; }
        }

        /// <summary>
        /// Append bit string to cell data.
        /// </summary>
        [JsonDiscriminator("BitString")]
        public class BitString : BuilderOp
        {
            /// <summary>
            /// Append bit string to cell data.
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Append ref to nested cells
        /// </summary>
        [JsonDiscriminator("Cell")]
        public class Cell : BuilderOp
        {
            /// <summary>
            /// Append ref to nested cells
            /// </summary>
            [JsonPropertyName("builder")]
            public JsonElement[] Builder { get; set; }
        }

        /// <summary>
        /// Append ref to nested cell
        /// </summary>
        [JsonDiscriminator("CellBoc")]
        public class CellBoc : BuilderOp
        {
            /// <summary>
            /// Append ref to nested cell
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }
        }
    }
}