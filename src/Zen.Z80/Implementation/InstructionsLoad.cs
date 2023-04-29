// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void LD_ann_R(byte[] parameters, Register source)
    {
        unchecked
        {
            var address = parameters.ReadLittleEndian();

            _interface.WriteToMemory(address, _state[source]);

            _state.MemPtr = (ushort) ((((parameters[1] << 8) | parameters[0] + 1) & 0xFF) | (_state[source] << 8));

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3, 3);
    }

    private void LD_ann_RR(byte[] parameters, RegisterPair source)
    {
        unchecked
        {
            var address = parameters.ReadLittleEndian();

            _interface.WriteToMemory(address, (byte) (_state[source] & 0xFF));

            _interface.WriteToMemory((ushort) (address + 1), (byte) ((_state[source] & 0xFF00) >> 8));

            _state.MemPtr = (ushort) ((parameters[1] << 8 | parameters[0]) + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3, 3, 3);
    }

    private void LD_aRR_n(RegisterPair target, byte[] parameters)
    {
        unchecked
        {
            var address = _state[target];

            _interface.WriteToMemory(address, parameters[0]);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3);
    }

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

    private void LD_aRRd_R(RegisterPair target, byte[] parameters, Register source)
    {
        unchecked
        {
            var address = _state[target];

            address = (ushort) (address + (sbyte) parameters[0]);

            _interface.WriteToMemory(address, _state[source]);

            _state.MemPtr = (ushort) (((_state[target] + 1) & 0xFF) | (_state[source] << 8));

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void LD_R_ann(Register target, byte[] parameters)
    {
        unchecked
        {
            var address = parameters.ReadLittleEndian();

            _state[target] = _interface.ReadFromMemory(address);

            _state.MemPtr = (ushort) (address + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3, 3);
    }

    private void LD_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            _state[target] = _interface.ReadFromMemory(address);

            _state.MemPtr = (ushort) (_state[source] + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void LD_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            _state[target] = _interface.ReadFromMemory(address);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void LD_R_n(Register target, byte[] parameters)
    {
        unchecked
        {
            _state[target] = parameters[0];

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

    private void LD_RR_ann(RegisterPair target, byte[] parameters)
    {
        unchecked
        {
            var address = parameters.ReadLittleEndian();

            var value = (ushort) _interface.ReadFromMemory(address);

            value |= (ushort) (_interface.ReadFromMemory((ushort) (address + 1)) << 8);

            _state[target] = value;

            _state.MemPtr = (ushort) ((parameters[1] << 8 | parameters[0]) + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3, 3, 3);
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

    private void LD_RR_RR(RegisterPair target, RegisterPair source)
    {
        unchecked
        {
            _state[target] = _state[source];

            _state.Q = 0;
        }

        _state.SetMCycles(6);
    }
}