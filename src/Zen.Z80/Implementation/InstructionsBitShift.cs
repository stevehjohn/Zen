// ReSharper disable InconsistentNaming
using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void RLC_R(Register register)
    {
        unchecked
        {
            var value = _state[register];

            var topBit = (byte) ((value & 0x80) >> 7);

            var result = (byte) (((value << 1) & 0xFE) | topBit);

            _state[register] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4);
    }
}