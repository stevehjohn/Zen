using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void IN_C()
    {
        unchecked
        {
            var data = _interface.ReadFromPort(_state[RegisterPair.BC]);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = data.IsEvenParity();
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = data == 0;
            _state[Flag.Sign] = (sbyte) data < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void IN_R_C(Register register)
    {
        unchecked
        {
            var data = _interface.ReadFromPort(_state[RegisterPair.BC]);

            _state[register] = data;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = data.IsEvenParity();
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = data == 0;
            _state[Flag.Sign] = (sbyte) data < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);
        }

        _state.SetMCycles(4, 4, 4);
    }
}