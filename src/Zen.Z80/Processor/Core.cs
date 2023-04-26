using Zen.Z80.Implementation;

namespace Zen.Z80.Processor;

public class Core
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Instructions _instructions;

    public Core(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;

        _instructions = new Instructions(_interface, _state);
    }

    public void ExecuteCycle()
    {
        _interface.Mreq = true;

        _interface.Address = _state.ProgramCounter;

        var opcode = _state.InstructionPrefix << 8 | _interface.Data;

        var instruction = _instructions[opcode];

        _state.LastInstruction = instruction;

        _state.InstructionPrefix = 0;

        _state.ProgramCounter++;

        var parameters = Array.Empty<byte>();

        if (instruction.ParameterLength > 0)
        {
            parameters = new byte[instruction.ParameterLength];

            for (var p = 0; p < instruction.ParameterLength; p++)
            {
                _interface.Mreq = true;

                _interface.Address = _state.ProgramCounter;

                _state.ProgramCounter++;

                parameters[p] = _interface.Data;
            }
        }

        instruction.Execute(parameters);

        if (_state.InstructionPrefix > 0xFF)
        {
            opcode = _state.InstructionPrefix << 8 | parameters[1];

            instruction = _instructions[opcode];

            instruction.Execute(parameters[..1]);

            _state.LastInstruction = instruction;
        }
    }
}