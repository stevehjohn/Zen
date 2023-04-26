using System.Diagnostics.CodeAnalysis;

namespace Zen.Z80.Test.JSMoo.Models;

[ExcludeFromCodeCoverage]
public class TestDefinition
{
    public string Name { get; set; }

    public StateDefinition Initial { get; set; }

    public StateDefinition Final { get; set; }

    public object[][] Cycles { get; set; }

    public object[][]? Ports { get; set; }
}
