using System.Diagnostics.CodeAnalysis;
using Zen.Z80.Test.JSMoo.Infrastructure;

namespace Zen.Z80.Test.JSMoo
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main()
        {
            TestRunner.RunTests();

            Console.ReadKey();
        }
    }
}