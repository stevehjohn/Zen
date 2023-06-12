using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618

namespace Zen.Z80.Test.JSMoo.Models;

[ExcludeFromCodeCoverage]
public struct TestDefinition
{
    public string Name { get; set; }

    public StateDefinition Initial { get; set; }

    public StateDefinition Final { get; set; }

    public object[][] Cycles { get; set; }

    public object[][]? Ports { get; set; }
}
