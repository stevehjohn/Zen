// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void LD_aRR_R(RegisterPair target, Register source)
    {
        unchecked
        {
            var address = _state[target];

            _interface.WriteToMemory(address, _state[source]);

            _state.MemPtr = (ushort) (((_state[target] + 1) & 0xFF) | (_state[source] << 8));

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void LD_R_R(Register target, Register source)
    {
        unchecked
        {
            var value = _state[source];

            _state[target] = value;

            if (target == Register.A && (source == Register.I || source == Register.R))
            {
                // Flags
                // Carry unaffected
                _state[Flag.AddSubtract] = false;
                _state[Flag.ParityOverflow] = _state.InterruptFlipFlop2;
                _state[Flag.X1] = (value & 0x08) > 0;
                _state[Flag.HalfCarry] = false;
                _state[Flag.X2] = (value & 0x20) > 0;
                _state[Flag.Zero] = value == 0;
                _state[Flag.Sign] = (sbyte) value < 0;
            }
            else
            {
                _state.Q = 0;
            }
        }

        _state.SetMCycles(4);
    }

    private void LD_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            _state[target] = _interface.ReadFromMemory(address);
        }

        _state.SetMCycles(4, 3);
    }

    private void LD_RR_nn(RegisterPair target, byte[] parameters)
    {
        unchecked
        {
            _state.LoadRegisterPair(target, parameters);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3);
    }
}