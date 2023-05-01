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
        if (_state.Halted)
        {
            HandleInterrupts();

            _instructions[0x00].Execute(Array.Empty<byte>());

            return;
        }

        var data = _interface.ReadFromMemory(_state.ProgramCounter);

        var opcode = _state.InstructionPrefix << 8 | data;

        var instruction = _instructions[opcode];

        if (! instruction.Mnemonic.StartsWith("PREFIX"))
        {
            _state.LastInstruction = instruction;
        }

        _state.InstructionPrefix = 0;
        
        _state.ProgramCounter++;

        var parameters = Array.Empty<byte>();

        if (instruction.ParameterLength > 0)
        {
            parameters = new byte[instruction.ParameterLength];

            for (var p = 0; p < instruction.ParameterLength; p++)
            {
                data = _interface.ReadFromMemory(_state.ProgramCounter);

                _state.ProgramCounter++;

                parameters[p] = data;
            }
        }
        
        UpdateR(instruction);

        instruction.Execute(parameters);

        if (_state.InstructionPrefix > 0xFF)
        {
            opcode = _state.InstructionPrefix << 8 | parameters[1];

            instruction = _instructions[opcode];

            UpdateR(instruction);

            instruction.Execute(parameters[..1]);

            _state.LastInstruction = instruction;

            _state.InstructionPrefix = 0;
        }
    }

    private void UpdateR(Instruction instruction)
    {
        if (instruction.Mnemonic.StartsWith("PREFIX"))
        {
            return;
        }

        var increment = 1;

        if (instruction.OpCode > 0xFF)
        {
            increment = 2;
        }

        var value = (byte) (_state[Register.R] & 0x7F);

        var topBit = _state[Register.R] & 0x80;

        value = (byte) (value + increment);

        _state[Register.R] = value;

        if (topBit > 0)
        {
            _state[Register.R] |= 0x80;
        }
        else
        {
            _state[Register.R] &= 0x7F;
        }
    }

    private void HandleInterrupts()
    {
        if (_interface.Interrupt)
        {
            HandleMaskableInterrupt();

            _interface.Interrupt = false;
        }
    }

    private void HandleMaskableInterrupt()
    {
        _state.Halted = false;

        if (! _state.InterruptFlipFlop1)
        {
            return;
        }

        switch (_state.InterruptMode)
        {
            case InterruptMode.IM1:
                PushProgramCounter();

                _state.ProgramCounter = 0x0038;

                break;
        }
    }

    private void PushProgramCounter()
    {
        _state.StackPointer--;

        _interface.WriteToMemory(_state.StackPointer, (byte) ((_state.ProgramCounter & 0xFF00) >> 8));

        _state.StackPointer--;

        _interface.WriteToMemory(_state.StackPointer, (byte) (_state.ProgramCounter & 0x00FFFF));
    }
}