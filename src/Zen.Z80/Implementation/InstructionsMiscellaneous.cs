// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void EX_RR_RR(RegisterPair left, RegisterPair right)
    {
        unchecked
        {
            (_state[left], _state[right]) = (_state[right], _state[left]);

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