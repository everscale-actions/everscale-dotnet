﻿using ch1seL.TonNet.Client;
using ch1seL.TonNet.RustAdapter;
using Microsoft.Extensions.Logging.Abstractions;

namespace ch1seL.TonNet.RustClient.Tests
{
    internal static class TestsHelpers
    {
        public static ITonClientRustAdapter CreateRustAdapter()
        {
            return new TonClientRustAdapter(new TonClientOptions(), NullLogger<TonClientRustAdapter>.Instance);
        }
    }
}