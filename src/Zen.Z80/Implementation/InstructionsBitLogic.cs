// ReSharper disable InconsistentNaming

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
    
    public void RL_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

            _interface.WriteToMemory(address, result);

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

    public void RLC_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _interface.WriteToMemory(address, result);

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
    
    public void RR_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _interface.WriteToMemory(address, result);

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state[Flag.Carry] = bottomBit  == 1;
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

    public void RRC_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _interface.WriteToMemory(address, result);

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state[Flag.Carry] = bottomBit  == 1;
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

    public void SLA_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) & 0xFE);

            _interface.WriteToMemory(address, result);

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

    public void SRA_aRRd_R(RegisterPair source, byte[] parameters, Register? target = null)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) (data & 0x80);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | topBit);

            _interface.WriteToMemory(address, result);

            if (target != null)
            {
                _state[(Register) target] = result;
            }

            _state[Flag.Carry] = bottomBit  == 1;
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