﻿// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void ADC_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left + right + carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) + carry > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void ADC_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left + right + carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) + carry > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void ADC_R_n(Register target, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var right = parameters[0];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left + right + carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) + carry > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void ADC_R_R(Register target, Register source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left + right + carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) + carry > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void ADC_RR_RR(RegisterPair target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left + right + carry;

            _state[target] = (ushort) result;

            _state[Flag.Carry] = result > 0xFFFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = (left & 0x8000) == ((right + carry) & 0x8000) && (left & 0x8000) != (result & 0x8000);
            _state[Flag.X1] = ((result >> 11) & 1) > 0;
            _state[Flag.HalfCarry] = (((left & 0x0FFF) + (right & 0x0FFF) + carry) & 0x1000) > 0;
            _state[Flag.X2] = ((result >> 13) & 1) > 0;
            _state[Flag.Zero] = (result & 0xFFFF) == 0;
            _state[Flag.Sign] = (short) result < 0;

            _state.MemPtr = (ushort) (_state[target] + 1);
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void ADD_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var result = left + right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void ADD_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var result = left + right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void ADD_R_n(Register target, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var right = parameters[0];

            var result = left + right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void ADD_R_R(Register target, Register source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var result = left + right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result > 0xFF;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) == 0 && ((left ^ result) & 0x80) != 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) + (right & 0x0F) > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void ADD_RR_RR(RegisterPair target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var result = left + right;

            _state[target] = (ushort) result;

            _state[Flag.Carry] = result > 0xFFFF;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (result & 0x0800) > 0;
            _state[Flag.HalfCarry] = (right & 0x0FFF) + (left & 0x0FFF) > 0x0FFF;
            _state[Flag.X2] = (result & 0x2000) > 0;
            // Zero unaffected
            // Sign unaffected

            _state.MemPtr = (ushort) (left + 1);
        }

        _state.SetMCycles(4, 4, 3);
    }

    private void AND_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var result = _state[target] & right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((byte) result).IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void AND_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var result = _state[target] & right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((byte) result).IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void AND_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var result = _state[register] & parameters[0];

            _state[register] = (byte) result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((byte) result).IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void AND_R_R(Register target, Register source)
    {
        unchecked
        {
            var result = _state[target] & _state[source];

            _state[target] = (byte) result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = ((byte) result).IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void CP_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var result = left - right;

            _state[Flag.Carry] = right > left;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ (byte) result) & 0x80) == 0;
            _state[Flag.X1] = (right & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (right & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (byte) result > 0x7F;
        }

        _state.SetMCycles(4, 3);
    }

    private void CP_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var result = left - right;

            _state[Flag.Carry] = right > left;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ (byte) result) & 0x80) == 0;
            _state[Flag.X1] = (right & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (right & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (byte) result > 0x7F;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void CP_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var left = _state[register];

            var right = parameters[0];

            var result = left - right;

            _state[Flag.Carry] = right > left;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ (byte) result) & 0x80) == 0;
            _state[Flag.X1] = (right & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (right & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (byte) result > 0x7F;
        }

        _state.SetMCycles(4, 3);
    }

    private void CP_R_R(Register target, Register source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var result = left - right;

            _state[Flag.Carry] = right > left;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ (byte) result) & 0x80) == 0;
            _state[Flag.X1] = (right & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (right & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (byte) result > 0x7F;
        }

        _state.SetMCycles(4);
    }

    private void DEC_aRR(RegisterPair registers)
    {
        unchecked
        {
            var address = _state[registers];

            var value = _interface.ReadFromMemory(address);

            var result = (byte) (value - 1);

            _interface.WriteToMemory(address, result);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = value == 0x80;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) == 0;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3);
    }

    private void DEC_aRRd(RegisterPair registers, byte[] parameters)
    {
        unchecked
        {
            var address = _state[registers];

            address = (ushort) (address + (sbyte) parameters[0]);

            var value = _interface.ReadFromMemory(address);

            var result = (byte) (value - 1);

            _interface.WriteToMemory(address, result);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = value == 0x80;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) == 0;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

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

    private void DEC_RR(RegisterPair registers)
    {
        unchecked
        {
            var value = _state[registers];

            var result = (ushort) (value - 1);

            _state[registers] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(6);
    }

    private void INC_aRR(RegisterPair registers)
    {
        unchecked
        {
            var address = _state[registers];

            var value = _interface.ReadFromMemory(address);

            var result = (byte) (value + 1);

            _interface.WriteToMemory(address, result);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = value == 0x7F;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) + 1 > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3);
    }

    private void INC_aRRd(RegisterPair registers, byte[] parameters)
    {
        unchecked
        {
            var address = _state[registers];

            address = (ushort) (address + (sbyte) parameters[0]);

            var value = _interface.ReadFromMemory(address);

            var result = (byte) (value + 1);

            _interface.WriteToMemory(address, result);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = value == 0x7F;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (value & 0x0F) + 1 > 0xF;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
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

    private void INC_RR(RegisterPair registers)
    {
        unchecked
        {
            var value = _state[registers];

            var result = (ushort) (value + 1);

            _state[registers] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(6);
    }

    private void SBC_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left - right - carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = (sbyte) left - (sbyte) right - carry is < -128 or > 127;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F) + carry;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void SBC_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left - right - carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = (sbyte) left - (sbyte) right - carry is < -128 or > 127;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F) + carry;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void SBC_R_n(Register target, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var right = parameters[0];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left - right - carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = (sbyte) left - (sbyte) right - carry is < -128 or > 127;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F) + carry;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void SBC_R_R(Register target, Register source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left - right - carry;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = (sbyte) left - (sbyte) right - carry is < -128 or > 127;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F) + carry;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void SBC_RR_RR(RegisterPair target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var carry = (byte) (_state[Flag.Carry] ? 1 : 0);

            var result = left - right - carry;

            _state[target] = (ushort) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ (right + carry)) & 0x8000) != 0 && (((right + carry) ^ (ushort) result) & 0x8000) == 0;
            _state[Flag.X1] = (result & 0x0800) > 0;
            _state[Flag.HalfCarry] = ((left & 0x0FFF) - (right & 0x0FFF) - carry & 0x1000) != 0;
            _state[Flag.X2] = (result & 0x2000) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (short) result < 0;

            _state.MemPtr = (ushort) (_state[target] + 1);
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SUB_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var right = _interface.ReadFromMemory(address);

            var result = left - right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ result) & 0x80) == 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void SUB_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var left = _state[target];

            var address = _state[source];

            var right = _interface.ReadFromMemory(address);

            var result = left - right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ result) & 0x80) == 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void SUB_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var left = _state[register];

            var right = parameters[0];

            var result = left - right;

            _state[register] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ result) & 0x80) == 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void SUB_R_R(Register target, Register source)
    {
        unchecked
        {
            var left = _state[target];

            var right = _state[source];

            var result = left - right;

            _state[target] = (byte) result;

            _state[Flag.Carry] = result < 0;
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = ((left ^ right) & 0x80) != 0 && ((right ^ result) & 0x80) == 0;
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = (left & 0x0F) < (right & 0x0F);
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = (byte) result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4);
    }

    private void OR_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data | _state[target]);

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

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void OR_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data | _state[target]);

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

        _state.SetMCycles(4, 3);
    }

    private void OR_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var result = (byte) (_state[register] | parameters[0]);

            _state[register] = result;

            _state[Flag.Carry] = false;
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result.IsEvenParity();
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (result & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 3);
    }

    private void OR_R_R(Register target, Register source)
    {
        unchecked
        {
            var result = (byte) (_state[source] | _state[target]);

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

    private void XOR_R_aRRd(Register target, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data ^ _state[target]);

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

        _state.SetMCycles(4, 4, 3, 5, 3);
    }

    private void XOR_R_aRR(Register target, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data ^ _state[target]);

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

        _state.SetMCycles(4, 3);
    }

    private void XOR_R_n(Register target, byte[] parameters)
    {
        unchecked
        {
            var result = (byte) (parameters[0] ^ _state[target]);

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

        _state.SetMCycles(4, 3);
    }

    private void XOR_R_R(Register target, Register source)
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