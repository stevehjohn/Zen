// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void ADC_R_aRR(Register target, RegisterPair source)
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

    public void ADC_R_R(Register target, Register source)
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

    public void ADD_R_aRR(Register target, RegisterPair source)
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

    public void ADD_R_R(Register target, Register source)
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

    public void SBC_R_aRR(Register target, RegisterPair source)
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

    public void SBC_R_R(Register target, Register source)
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

    public void SUB_R_aRR(Register target, RegisterPair source)
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

    public void SUB_R_R(Register target, Register source)
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

    //private void DEC_aRR(RegisterPair register)
    //{
    //    unchecked
    //    {
    //        var address = _state[register];

    //        var value = _interface.ReadFromMemory(address);

    //        var result = (byte) (value - 1);

    //        _interface.WriteToMemory(address, result);

    //        // Carry unaffected
    //        _state[Flag.AddSubtract] = true;
    //        _state[Flag.ParityOverflow] = value == 0x80;
    //        _state[Flag.X1] = (result & 0x08) > 0;
    //        _state[Flag.HalfCarry] = (value & 0x0F) == 0;
    //        _state[Flag.X2] = (result & 0x20) > 0;
    //        _state[Flag.Zero] = result == 0;
    //        _state[Flag.Sign] = (sbyte) result < 0;
    //    }

    //    _state.SetMCycles(4, 4, 3);
    //}

    //private void DEC_R(Register register)
    //{
    //    unchecked
    //    {
    //        var value = _state[register];

    //        var result = (byte) (value - 1);

    //        _state[register] = result;

    //        // Carry unaffected
    //        _state[Flag.AddSubtract] = true;
    //        _state[Flag.ParityOverflow] = value == 0x80;
    //        _state[Flag.X1] = (result & 0x08) > 0;
    //        _state[Flag.HalfCarry] = (value & 0x0F) == 0;
    //        _state[Flag.X2] = (result & 0x20) > 0;
    //        _state[Flag.Zero] = result == 0;
    //        _state[Flag.Sign] = (sbyte) result < 0;
    //    }

    //    _state.SetMCycles(4);
    //}

    //private void INC_aRR(RegisterPair register)
    //{
    //    unchecked
    //    {
    //        var address = _state[register];

    //        var value = _interface.ReadFromMemory(address);

    //        var result = (byte) (value + 1);

    //        _interface.WriteToMemory(address, result);

    //        // Carry unaffected
    //        _state[Flag.AddSubtract] = false;
    //        _state[Flag.ParityOverflow] = value == 0x7F;
    //        _state[Flag.X1] = (result & 0x08) > 0;
    //        _state[Flag.HalfCarry] = (value & 0x0F) + 1 > 0xF;
    //        _state[Flag.X2] = (result & 0x20) > 0;
    //        _state[Flag.Zero] = result == 0;
    //        _state[Flag.Sign] = (sbyte) result < 0;
    //    }

    //    _state.SetMCycles(4, 4, 3);
    //}

    //private void INC_R(Register register)
    //{
    //    unchecked
    //    {
    //        var value = _state[register];

    //        var result = (byte) (value + 1);

    //        _state[register] = result;

    //        // Carry unaffected
    //        _state[Flag.AddSubtract] = false;
    //        _state[Flag.ParityOverflow] = value == 0x7F;
    //        _state[Flag.X1] = (result & 0x08) > 0;
    //        _state[Flag.HalfCarry] = (value & 0x0F) + 1 > 0xF;
    //        _state[Flag.X2] = (result & 0x20) > 0;
    //        _state[Flag.Zero] = result == 0;
    //        _state[Flag.Sign] = (sbyte) result < 0;
    //    }

    //    _state.SetMCycles(4);
    //}
}