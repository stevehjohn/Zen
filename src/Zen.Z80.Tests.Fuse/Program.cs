using System.Diagnostics.CodeAnalysis;
using Zen.Z80.Tests.Fuse.Infrastructure;

namespace Zen.Z80.Tests.Fuse;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main()
    {
        var runner = new TestRunner();

        runner.RunTests();
    }
}