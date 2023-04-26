using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Zen.Z80.Test.JSMoo.Models;

namespace Zen.Z80.Test.JSMoo.Infrastructure;

[ExcludeFromCodeCoverage]
public class TestRunner
{
    public static void RunTests()
    {
        var files = Directory.EnumerateFiles("TestDefinitions", "*.json");

        var total = 0;

        var passed = 0;

        var notImplemented = 0;

        Console.CursorVisible = false;

        var stopwatch = Stopwatch.StartNew();

        var failedNames = new List<string>();
        
        var notImplementedNames = new List<string>();

        foreach (var file in files)
        {
            var tests = JsonSerializer.Deserialize<TestDefinition[]>(File.ReadAllText(file), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}