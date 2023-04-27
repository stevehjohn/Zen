// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void DEC_R(Register register)
    {
        unchecked
        {
            var value = _state[register];

            var result = (byte) (value - 1);

            _state[register] = result;

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = value == 0x80;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) == 0;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void INC_R(Register register)
    {
        unchecked
        {
            var value = _state[register];

            var result = (byte) (value + 1);

            _state[register] = result;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = value == 0x7F;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) + 1 > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }
}