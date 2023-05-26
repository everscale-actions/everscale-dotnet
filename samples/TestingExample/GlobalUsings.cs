// Global using directives

global using System.Text.Json;
global using EverscaleNet;
global using EverscaleNet.Adapter.Rust;
global using EverscaleNet.Client;
global using EverscaleNet.Client.PackageManager;
global using EverscaleNet.Exceptions;
global using EverscaleNet.TestSuite.Accounts;
global using EverscaleNet.TestSuite.Giver;
global using EverscaleNet.TestSuite.Services;
global using FluentAssertions;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Polly;
global using Polly.Retry;
global using Serilog;
global using Serilog.Extensions.Logging;
global using TestingExample.Accounts;
global using Xunit;
global using Xunit.DependencyInjection;
global using Xunit.DependencyInjection.Logging;
