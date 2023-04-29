// ReSharper disable InconsistentNaming

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

    private void IN_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var address = (ushort) ((_state[register] << 8) | parameters[0]);

            var data = _interface.ReadFromPort(address);

            _state[register] = data;

            _state.MemPtr = (ushort) (address + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4);
    }

    private void OUT_n_R(Register register, byte[] parameters)
    {
        unchecked
        {
            var address = (ushort) ((_state[register] << 8) | parameters[0]);

            _interface.WriteToPort(address, _state[register]);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4);
    }

    private void OUT_C_r(Register register)
    {
        unchecked
        {
            _interface.WriteToPort(_state[RegisterPair.BC], _state[register]);

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void OUT_C_0()
    {
        unchecked
        {
            _interface.WriteToPort(_state[RegisterPair.BC], 0);

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4);
    }
}