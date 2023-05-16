// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void CPD()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var difference = (sbyte) (_state[Register.A] - value);

            _state[RegisterPair.HL]--;

            _state[RegisterPair.BC]--;

            var x = difference - (_state[Flag.Carry] ? 1 : 0);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = (((_state[Register.A] & 0x0F) - (value & 0x0F)) & 0x10) > 0;
            _state[Flag.X2] = (x & 0x02) > 0;
            _state[Flag.Zero] = difference == 0;
            _state[Flag.Sign] = (byte) difference > 0x7F;

            _state.MemPtr++;
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void CPDR()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var difference = (sbyte) (_state[Register.A] - value);

            _state[RegisterPair.HL]--;

            _state[RegisterPair.BC]--;

            var x = _state[Register.A] - (_state[Flag.Carry] ? 1 : 0);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (value & 0x0F);
            _state[Flag.X2] = (x & 0x20) > 0; // TODO: 0x02?
            _state[Flag.Zero] = difference == 0;
            _state[Flag.Sign] = (byte) difference > 0x7F;

            if (_state[RegisterPair.BC] == 1 || difference == 0)
            {
                _state.MemPtr++;
            }
            else
            {
                _state.MemPtr = (ushort) (_state.ProgramCounter + 1);
            }

            if (_state[RegisterPair.BC] != 0 && difference != 0)
            {
                _state[Flag.X1] = (_state.ProgramCounter & 0x0800) > 0;
                _state[Flag.X2] = (_state.ProgramCounter & 0x2000) > 0;

                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 4, 3, 5, 5);

                return;
            }
        }

        _state.SetMCycles(4, 4, 3, 5);
    }
    
    private void CPI()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var difference = _state[Register.A] - value;

            _state[RegisterPair.HL]++;

            _state[RegisterPair.BC]--;

            var x = _state[Register.A] - value - (_state[Flag.HalfCarry] ? 1 : 0);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (value & 0x0F);
            _state[Flag.X2] = (x & 0x02) > 0;
            _state[Flag.Zero] = difference == 0;
            _state[Flag.Sign] = (byte) difference > 0x7F;

            _state.MemPtr++;
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void CPIR()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var difference = _state[Register.A] - value;

            _state[RegisterPair.HL]++;

            _state[RegisterPair.BC]--;

            var x = difference - (_state[Flag.HalfCarry] ? 1 : 0);

            // Carry unaffected
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (x & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (value & 0x0F);
            _state[Flag.X2] = (x & 0x02) > 0;
            _state[Flag.Zero] = difference == 0;
            _state[Flag.Sign] = (byte) difference > 0x7F;

            if (_state[RegisterPair.BC] == 1 || difference == 0)
            {
                _state.MemPtr++;
            }
            else
            {
                _state.MemPtr = (ushort) (_state.ProgramCounter + 1);
            }

            if (_state[RegisterPair.BC] != 0 && difference != 0)
            {
                _state[Flag.X1] = (_state.ProgramCounter & 0x0800) > 0;
                _state[Flag.X2] = (_state.ProgramCounter & 0x2000) > 0;

                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 4, 3, 5, 5);

                return;
            }
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void DI()
    {
        _state.InterruptFlipFlop1 = false;

        _state.InterruptFlipFlop2 = false;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void EI()
    {
        _state.InterruptFlipFlop1 = true;

        _state.InterruptFlipFlop2 = true;

        _state.IgnoreNextInterrupt = true;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void EX_aRR_RR(RegisterPair left, RegisterPair right)
    {
        unchecked
        {
            var value = _state[right];

            var data = (ushort) (_interface.ReadFromMemory((ushort) (_state[left] + 1)) << 8);

            data |= _interface.ReadFromMemory(_state[left]);

            _state[right] = data;

            _interface.WriteToMemory((ushort) (_state[left] + 1), (byte) ((value & 0xFF00) >> 8));

            _interface.WriteToMemory(_state[left], (byte) (value & 0x00FF));

            _state.MemPtr = _state[right];

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4, 3, 5);
    }

    private void EX_RR_RR(RegisterPair left, RegisterPair right)
    {
        unchecked
        {
            (_state[left], _state[right]) = (_state[right], _state[left]);

            _state.Q = 0;
        }

        _state.SetMCycles(4);
    }

    private void EXX()
    {
        unchecked
        {
            (_state[RegisterPair.BC], _state[RegisterPair.BC_]) = (_state[RegisterPair.BC_], _state[RegisterPair.BC]);

            (_state[RegisterPair.DE], _state[RegisterPair.DE_]) = (_state[RegisterPair.DE_], _state[RegisterPair.DE]);

            (_state[RegisterPair.HL], _state[RegisterPair.HL_]) = (_state[RegisterPair.HL_], _state[RegisterPair.HL]);

            _state.Q = 0;
        }

        _state.SetMCycles(4);
    }

    private void HALT()
    {
        _state.Halted = true;

        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void IM(InterruptMode mode)
    {
        _state.InterruptMode = mode;

        _state.Q = 0;

        _state.SetMCycles(4, 4);
    }

    private void LDD()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            _interface.WriteToMemory(_state[RegisterPair.DE], value);

            _state[RegisterPair.HL]--;

            _state[RegisterPair.DE]--;

            _state[RegisterPair.BC]--;

            value += _state[Register.A];

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (value & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (value & 0x20) > 0; // TODO: 0x02?
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void LDDR()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            _interface.WriteToMemory(_state[RegisterPair.DE], value);

            _state[RegisterPair.HL]--;

            _state[RegisterPair.DE]--;

            _state[RegisterPair.BC]--;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (_state.ProgramCounter & 0x0800) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (_state.ProgramCounter & 0x2000) > 0;
            // Zero unaffected
            // Sign unaffected

            if (_state[RegisterPair.BC] != 1)
            {
                _state.MemPtr = (ushort) (_state.ProgramCounter + 1);
            }

            if (_state[RegisterPair.BC] != 0)
            {
                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 4, 3, 5, 5);

                return;
            }
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void LDI()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            _interface.WriteToMemory(_state[RegisterPair.DE], value);

            _state[RegisterPair.HL]++;

            _state[RegisterPair.DE]++;

            _state[RegisterPair.BC]--;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (_state.ProgramCounter & 0x0800) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (_state.ProgramCounter & 0x2000) > 0;
            // Zero unaffected
            // Sign unaffected
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void LDIR()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            _interface.WriteToMemory(_state[RegisterPair.DE], value);

            _state[RegisterPair.HL]++;

            _state[RegisterPair.DE]++;

            _state[RegisterPair.BC]--;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = ((value + _state[Register.A]) & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = ((value + _state[Register.A]) & 0x02) > 0;
            // Zero unaffected
            // Sign unaffected

            if (_state[RegisterPair.BC] != 1)
            {
                _state.MemPtr = (ushort) (_state.ProgramCounter + 1);
            }

            if (_state[RegisterPair.BC] != 0)
            {
                _state.ProgramCounter -= 2;

                //_state[Flag.X1] = (_state.ProgramCounter & 0x0800) > 0;
                //_state[Flag.X2] = (_state.ProgramCounter & 0x2000) > 0;

                _state.SetMCycles(4, 4, 3, 5, 5);

                return;
            }
        }

        _state.SetMCycles(4, 4, 3, 5);
    }

    private void NOP()
    {
        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void PREFIX(ushort parameters)
    {
        _state.InstructionPrefix = parameters;

        _state.ClearMCycles();
    }

    private void RLD()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var al = (byte) (_state[Register.A] & 0x0F);

            var ah = (byte) (_state[Register.A] & 0xF0);

            var vl = (byte) (value & 0x0F);

            var vh = (byte) (value & 0xF0);

            _state[Register.A] = (byte) (ah | vh >> 4);

            value = (byte) ((vl << 4) | al);

            _interface.WriteToMemory(_state[RegisterPair.HL], value);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[Register.A].IsEvenParity();
            _state[Flag.X1] = (_state[Register.A] & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (_state[Register.A] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.A] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.A] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.HL] + 1);
        }

        _state.SetMCycles(4, 4, 3, 4, 3);
    }

    private void RRD()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            var al = (byte) (_state[Register.A] & 0x0F);

            var ah = (byte) (_state[Register.A] & 0xF0);

            var vl = (byte) (value & 0x0F);

            var vh = (byte) (value & 0xF0);

            _state[Register.A] = (byte) (ah | vl);

            value = (byte) ((al << 4) | (vh >> 4));

            _interface.WriteToMemory(_state[RegisterPair.HL], value);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[Register.A].IsEvenParity();
            _state[Flag.X1] = (_state[Register.A] & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (_state[Register.A] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.A] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.A] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.HL] + 1);
        }

        _state.SetMCycles(4, 4, 3, 4, 3);
    }
}