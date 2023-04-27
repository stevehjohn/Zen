// ReSharper disable InconsistentNaming

using System.Net;
using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void BIT_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (address & 0x0800) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (address & 0x2000) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4);
    }

    public void RES_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void SET_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data | bit);

            _interface.WriteToMemory(address, result);

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void XOR_R_R(Register target, Register source)
    {
        unchecked
        {
            var result = (byte) (_state[source] ^ _state[target]);

            _state[target] = result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }
}