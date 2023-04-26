// ReSharper disable InconsistentNaming

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
        }

        _state.SetMCycles(4, 4);
    }
}