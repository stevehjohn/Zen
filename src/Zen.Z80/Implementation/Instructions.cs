using System.Diagnostics.CodeAnalysis;
using Zen.Z80.Exceptions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

[ExcludeFromCodeCoverage]
public partial class Instructions
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Dictionary<int, Instruction> _instructions = new();

    public Instructions(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;

        Initialise();
    }

    public Instruction this[int opCode]
    {
        get
        {
            if (! _instructions.ContainsKey(opCode))
            {
                if (opCode > 0xFF && _instructions.ContainsKey(opCode & 0xFF))
                {
                    var instruction = _instructions[opCode & 0xFF];

                    return new Instruction(instruction.Execute, instruction.Mnemonic, instruction.OpCode, instruction.ParameterLength, (byte) (instruction.ParameterLength + 4));
                }

                throw new OpCodeNotFoundException($"OpCode not found: {opCode:X8}");
            }

            return _instructions[opCode]; 
        }
    }
}