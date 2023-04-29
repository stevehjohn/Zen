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