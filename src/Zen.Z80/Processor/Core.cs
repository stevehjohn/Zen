#define LOG

using Zen.Z80.Implementation;

namespace Zen.Z80.Processor;

public class Core
{
#if LOG
    private const int LogBufferSize = 20_000;

    private const string LogFile = "Zen.log";

    private long _opcodesExecuted;
#endif

    private readonly Interface _interface;

    private readonly State _state;

    private readonly Instructions _instructions;

#if LOG
    private readonly List<string> _log = new(LogBufferSize);
#endif

    public Core(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;

        _instructions = new Instructions(_interface, _state);

#if LOG
        File.Delete(LogFile);
#endif
    }

    public void ExecuteCycle()
    {
        if (_state.Halted)
        {
            HandleInterrupts();

            _instructions[0x00].Execute(Array.Empty<byte>());

            _opcodesExecuted++;

            Log(_instructions[0x00]);

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
        
        _opcodesExecuted++;

#if LOG
        Log(instruction);
#endif

        if (_state.InstructionPrefix > 0xFF)
        {
            opcode = _state.InstructionPrefix << 8 | parameters[1];

            instruction = _instructions[opcode];

            UpdateR(instruction);

            instruction.Execute(parameters[..1]);
            
            _opcodesExecuted++;

#if LOG
            Log(instruction);
#endif

            _state.LastInstruction = instruction;

            _state.InstructionPrefix = 0;
        }

        if (! instruction.Mnemonic.StartsWith("PREFIX") && _state.InstructionPrefix == 0)
        {
            HandleInterrupts();
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

        _state.InterruptFlipFlop1 = _state.InterruptFlipFlop2 = false;

        switch (_state.InterruptMode)
        {
            case InterruptMode.IM0:
                throw new Exception("Not implemented - not used by Spectrum");

            case InterruptMode.IM1:
                PushProgramCounter();

                _state.ProgramCounter = 0x0038;

                break;

            case InterruptMode.IM2:
                _log.Add("INT");

                PushProgramCounter();

                // TODO: Get 0xFF from bus.
                var address = (_state[Register.I] << 8) | 0xFF;

                _state.ProgramCounter = (ushort) (_interface.ReadFromMemory((ushort) address) | (_interface.ReadFromMemory((ushort) (address + 1)) << 8));

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

#if LOG
    private void Log(Instruction instruction)
    {
        _log.Add($"OE: {_opcodesExecuted:X8}  TS: {_state.ClockCycles:X2}  PC: {_state.ProgramCounter:X8}  SP: {_state.StackPointer}  AF: {_state[Register.A]:X2}{_state[Register.F] & 0b1101_0111:X2}  BC: {_state[RegisterPair.BC]:X4}  DE: {_state[RegisterPair.DE]:X4}  HL: {_state[RegisterPair.HL]:X4}  OP: {instruction.OpCode:X8}  {instruction.Mnemonic}");

        //if (instruction.Mnemonic == "IN A, (C)")
        //{
        //    _log.Add($"A: {_state[Register.A]:X2}  BC: {_state[RegisterPair.BC]}  (BC): {_interface.ReadPort(_state[RegisterPair.BC])}");
        //}

        if (_log.Count == LogBufferSize)
        {
            File.AppendAllLines(LogFile, _log);

            _log.Clear();
        }
    }
#endif
}