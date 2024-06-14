// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void BIT_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
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

    private void BIT_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

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
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void BIT_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = bit == 0x80 && result != 0;
        }

        _state.SetMCycles(4, 4);
    }

    private void CCF()
    {
        unchecked
        {
            var value = _state[Flag.Carry];

            var x = (byte) ((_state.Q ^ _state[Register.F]) | _state[Register.A]);

            _state[Flag.Carry] = ! value;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = value;
            _state[Flag.X2] = (x & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }

    private void CPL()
    {
        unchecked
        {
            var result = _state[Register.A] ^ 0xFF;

            _state[Register.A] = (byte) result;

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            // ParityOverflow unaffected
            _state[Flag.X1] = (result & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (result & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }

    private void DAA()
    {
        unchecked
        {
            var adjust = 0;

            if (_state[Flag.HalfCarry] || (_state[Register.A] & 0x0F) > 0x09)
            {
                adjust++;
            }

            if (_state[Flag.Carry] || _state[Register.A] > 0x99)
            {
                adjust += 2;

                _state[Flag.Carry] = true;
            }

            if (_state[Flag.AddSubtract] && ! _state[Flag.HalfCarry])
            {
                _state[Flag.HalfCarry] = false;
            }
            else
            {
                if (_state[Flag.AddSubtract] && _state[Flag.HalfCarry])
                {
                    _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < 0x06;
                }
                else
                {
                    _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) >= 0x0A;
                }
            }

            switch (adjust)
            {
                case 1:
                    _state[Register.A] += (byte) (_state[Flag.AddSubtract] ? 0xFA : 0x06);

                    break;
                case 2:
                    _state[Register.A] += (byte) (_state[Flag.AddSubtract] ? 0xA0 : 0x60);

                    break;
                case 3:
                    _state[Register.A] += (byte) (_state[Flag.AddSubtract] ? 0x9A : 0x66);

                    break;
            }

            // Flags
            // Carry adjusted by operation
            // AddSubtract adjusted by operation
            _state[Flag.ParityOverflow] = _state[Register.A].IsEvenParity();
            _state[Flag.X1] = (_state[Register.A] & 0x08) > 0;
            // HalfCary adjusted by operation
            _state[Flag.X2] = (_state[Register.A] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.A] == 0;
            _state[Flag.Sign] = (_state[Register.A] & 0x80) > 0;
        }

        _state.SetMCycles(4);
    }

    private void RES_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RES_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void RES_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void RES_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data & ~bit);

            _state[source] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4);
    }

    private void SCF()
    {
        unchecked
        {
            var x = (byte) ((_state.Q ^ _state[Register.F]) | _state[Register.A]);

            _state[Flag.Carry] = true;
            _state[Flag.AddSubtract] = false;
            // ParityOverflow unaffected
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (x & 0x20) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4);
    }

    private void SET_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4, 3);
    }

    private void SET_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort)(address + (sbyte)parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SET_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort)(address + (sbyte)parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    private void SET_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data | bit);

            _state[source] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4);
    }
}