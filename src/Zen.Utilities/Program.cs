using System.Diagnostics.CodeAnalysis;
using Zen.Utilities.Tools;
using Zen.Utilities.Visualisations;

namespace Zen.Utilities;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        switch (args[0].ToLower())
        {
            case "table":
                var generator = new InstructionTableGenerator();
                generator.Generate();
                break;

            case "code":
                var codeGenerator = new CodeGenerator();
                codeGenerator.GenerateOpCodeInitialisers();
                break;

            default:
                var emitter = new JsonOpcodeEmitter();
                emitter.Emit();
                break;
        }
    }
}