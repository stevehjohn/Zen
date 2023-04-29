// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void DI()
    {
        _state.InterruptFlipFlop1 = false;

        _state.InterruptFlipFlop2 = false;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void EI()
    {
        _state.InterruptFlipFlop1 = true;

        _state.InterruptFlipFlop2 = true;

        _state.IgnoreNextInterrupt = true;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void EX_aSP_RR(RegisterPair registers)
    {
        unchecked
        {
            var value = _state[registers];

            var data = (ushort) (_interface.ReadFromMemory((ushort) (_state.StackPointer + 1)) << 8);

            data |= _interface.ReadFromMemory(_state.StackPointer);

            _state[registers] = data;

            _interface.WriteToMemory((ushort) (_state.StackPointer + 1), (byte) ((value & 0xFF00) >> 8));

            _interface.WriteToMemory(_state.StackPointer, (byte) (value & 0x00FF));

            _state.MemPtr = _state[registers];

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4, 3, 5);
    }

    private void EX_RR_RR(RegisterPair left, RegisterPair right)
    {
        unchecked
        {
            (_state[left], _state[right]) = (_state[right], _state[left]);

            _state.Q = 0;
        }

        _state.SetMCycles(4);
    }

    private void EXX()
    {
        unchecked
        {
            (_state[RegisterPair.BC], _state[RegisterPair.BC_]) = (_state[RegisterPair.BC_], _state[RegisterPair.BC]);

            (_state[RegisterPair.DE], _state[RegisterPair.DE_]) = (_state[RegisterPair.DE_], _state[RegisterPair.DE]);

            (_state[RegisterPair.HL], _state[RegisterPair.HL_]) = (_state[RegisterPair.HL_], _state[RegisterPair.HL]);

            _state.Q = 0;
        }

        _state.SetMCycles(4);
    }

    private void HALT()
    {
        _state.ProgramCounter--;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void IM(InterruptMode mode)
    {
        _state.InterruptMode = mode;

        _state.Q = 0;

        _state.SetMCycles(4, 4);
    }

    private void NOP()
    {
        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void PREFIX(ushort parameters)
    {
        _state.InstructionPrefix = parameters;

        _state.SetMCycles(0);
    }
}