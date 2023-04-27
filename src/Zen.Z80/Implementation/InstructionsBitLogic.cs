// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void BIT_b_IX_d(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            _interface.Mreq = true;

            _interface.Address = address;

            var data = _interface.Data;

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

    public void RES_b_IX_d_R(byte bit, RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            _interface.Mreq = true;

            _interface.Address = address;
            
            var data = _interface.Data;

            var result = (byte) (data & ~bit);

            _interface.TransferType = TransferType.Write;

            _interface.Data = result;

            // TODO: Data changed event or something?
            _interface.Address = address;

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void RLC_IX_d_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            _interface.Mreq = true;

            _interface.Address = address;

            var data = _interface.Data;

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _interface.TransferType = TransferType.Write;

            _interface.Data = result;

            // TODO: Data changed event or something?
            _interface.Address = address;

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (address & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (address & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
}