// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
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

    private void EX_aSP_RR(RegisterPair registers)
    {
        unchecked
        {
            var value = _state[registers];

            var data = (ushort) (_interface.ReadFromMemory((ushort) (_state.StackPointer + 1)) << 8);

            data |= _interface.ReadFromMemory(_state.StackPointer);

            _state[registers] = data;

            _interface.WriteToMemory((ushort) (_state.StackPointer + 1), (byte) ((value & 0xFF00) >> 8));

            _interface.WriteToMemory(_state.StackPointer, (byte) (value & 0x00FF));

            _state.MemPtr = _state[registers];

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
        _state.ProgramCounter--;

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

            value += _state[Register.A];

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (value & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (value & 0x20) > 0; // TODO: 0x02?
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

    private void LDIR()
    {
        unchecked
        {
            var value = _interface.ReadFromMemory(_state[RegisterPair.HL]);

            _interface.WriteToMemory(_state[RegisterPair.DE], value);

            _state[RegisterPair.HL]++;

            _state[RegisterPair.DE]++;

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

    private void NOP()
    {
        _state.Q = 0;

        _state.SetMCycles(4);
    }

    private void PREFIX(ushort parameters)
    {
        _state.InstructionPrefix = parameters;

        _state.SetMCycles(0);
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