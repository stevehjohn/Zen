using System.Diagnostics.CodeAnalysis;
using Zen.Utilities.Visualisations;

namespace Zen.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main()
        {
            // TODO: Add CommandLineParser when required...
            var generator = new InstructionTableGenerator();

            generator.Generate();

            //var codeGenerator = new CodeGenerator();

            //codeGenerator.GenerateOpCodeInitialisers();

            //var emitter = new JsonOpcodeEmitter();

            //emitter.Emit();
        }
    }
}