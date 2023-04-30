// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void NEG()
    {
        unchecked
        {
            var value = _state[Register.A];

            var result = (byte) ~value;

            result++;

            _state[Register.A] = result;

            _state[Flag.Carry] = value != 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = value == 0x80;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) + (result & 0x0F) > 0x0F;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (result & 0x80) > 0;
        }

        _state.SetMCycles(4, 4);
    }

    private void RL_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void RL_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RL_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RLA()
    {
        unchecked
        {
            var data = _state[Register.A];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

            _state[Register.A] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }

    private void RL_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | (byte) (_state[Flag.Carry] ? 0x01 : 0x00));

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

    private void RLC_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void RLC_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RLC_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    private void RLCA()
    {
        unchecked
        {
            var data = _state[Register.A];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

            _state[Register.A] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }
    
    private void RLC_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | topBit);

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

    private void RR_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RR_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void RR_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    private void RRA()
    {
        unchecked
        {
            var data = _state[Register.A];

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _state[Register.A] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }
    
    private void RR_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (byte) (_state[Flag.Carry] ? 0x80 : 0x00));

            _state[register] = result;

            _state[Flag.Carry] = bottomBit == 1;
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
    
    private void RRCA()
    {
        unchecked
        {
            var data = _state[Register.A];

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _state[Register.A] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            // Parity unaffected
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }

    private void RRC_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _state[register] = result;

            _state[Flag.Carry] = bottomBit == 1;
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
    
    private void RRC_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void RRC_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RRC_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | (bottomBit << 7));

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SLA_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) & 0xFE);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SLA_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) & 0xFE);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SLA_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) & 0xFE);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    private void SLA_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) ((data << 1) & 0xFE);

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

    private void SLL_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | 0x01);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SLL_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | 0x01);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    private void SLL_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | 0x01);

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

    private void SLL_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) ((data & 0x80) >> 7);

            var result = (byte) (((data << 1) & 0xFE) | 0x01);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = topBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SRA_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var topBit = (byte) (data & 0x80);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | topBit);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SRA_aRRd(RegisterPair source, byte[] parameters)
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

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SRA_aRRd_R(RegisterPair source, byte[] parameters, Register target)
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

            _state[target] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    private void SRA_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var topBit = (byte) (data & 0x80);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) | topBit);

            _state[register] = result;

            _state[Flag.Carry] = bottomBit == 1;
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

    private void SRL_aRR(RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) & 0x7F);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SRL_aRRd(RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) & 0x7F);

            _interface.WriteToMemory(address, result);

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SRL_aRRd_R(RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) & 0x7F);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state[Flag.Carry] = bottomBit == 1;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SRL_R(Register register)
    {
        unchecked
        {
            var data = _state[register];

            var bottomBit = (byte) (data & 0x01);

            var result = (byte) ((data >> 1) & 0x7F);

            _state[register] = result;

            _state[Flag.Carry] = bottomBit == 1;
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