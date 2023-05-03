// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void IN_C()
    {
        unchecked
        {
            var data = _interface.ReadFromPort(_state[RegisterPair.BC]);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = data.IsEvenParity();
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = data == 0;
            _state[Flag.Sign] = (sbyte) data < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void IN_R_C(Register register)
    {
        unchecked
        {
            var data = _interface.ReadFromPort(_state[RegisterPair.BC]);

            _state[register] = data;

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = data.IsEvenParity();
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = data == 0;
            _state[Flag.Sign] = (sbyte) data < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void IN_R_n(Register register, byte[] parameters)
    {
        unchecked
        {
            var address = (ushort) ((_state[register] << 8) | parameters[0]);

            var data = _interface.ReadFromPort(address);

            _state[register] = data;

            _state.MemPtr = (ushort) (address + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4);
    }

    private void IND()
    {
        unchecked
        {
            var port = _state[RegisterPair.BC];

            var data = _interface.ReadFromPort(port);

            _interface.WriteToMemory(_state[RegisterPair.HL], data);

            _state[RegisterPair.HL]--;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (port + 1);
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void INDR()
    {
        unchecked
        {
            var port = _state[RegisterPair.BC];

            var data = _interface.ReadFromPort(port);

            _interface.WriteToMemory(_state[RegisterPair.HL], data);

            _state[RegisterPair.HL]--;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (port + 1);

            if (_state[Register.B] != 0)
            {
                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 5, 3, 4, 5);

                return;
            }
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void INI()
    {
        unchecked
        {
            var port = _state[RegisterPair.BC];

            var data = _interface.ReadFromPort(port);

            _interface.WriteToMemory(_state[RegisterPair.HL], data);

            _state[RegisterPair.HL]++;

            _state[Register.B]--;

            _state[Flag.Carry] = data + ((_state[Register.C] + 1) & 0xFF) > 0xFF;
            _state[Flag.AddSubtract] = (data & 0x80) > 0;
            _state[Flag.ParityOverflow] = ((ushort) (((data + ((_state[Register.C] + 1) & 0xFF)) & 0x07) ^ _state[Register.B])).IsEvenParity();
            _state[Flag.X1] = (_state[Register.B] & 0x08) > 0;
            _state[Flag.HalfCarry] = data + ((_state[Register.C] + 1) & 0xFF) > 0xFF;
            _state[Flag.X2] = (_state[Register.B] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (port + 1);
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void INIR()
    {
        unchecked
        {
            var port = _state[RegisterPair.BC];

            var data = _interface.ReadFromPort(port);

            _interface.WriteToMemory(_state[RegisterPair.HL], data);

            _state[RegisterPair.HL]++;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (port + 1);

            if (_state[RegisterPair.BC] != 0)
            {
                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 5, 3, 4, 5);

                return;
            }
        }

        _state.SetMCycles(4, 5, 3, 4);
    }
    
    private void OTDR()
    {
        unchecked
        {
            var address = _state[RegisterPair.HL];

            var data = _interface.ReadFromMemory(address);

            _interface.WriteToPort(address, data);

            _state[RegisterPair.HL]--;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            if (_state[Register.B] != 0)
            {
                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 5, 3, 4, 5);

                return;
            }
        }

        _state.SetMCycles(4, 5, 3, 4);
    }
    
    private void OTIR()
    {
        unchecked
        {
            var address = _state[RegisterPair.HL];

            var data = _interface.ReadFromMemory(address);

            var port = _state[RegisterPair.BC];

            _interface.WriteToPort(port, data);

            _state[RegisterPair.HL]++;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = (data & 0x80) > 0;
            _state[Flag.ParityOverflow] = _state[Register.B].IsEvenParity();
            _state[Flag.X1] = (_state[Register.B] & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (_state[Register.B] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            if (_state[Register.B] != 0)
            {
                _state.ProgramCounter -= 2;

                _state.SetMCycles(4, 5, 3, 4, 5);

                return;
            }
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void OUTD()
    {
        unchecked
        {
            var address = _state[RegisterPair.HL];

            var data = _interface.ReadFromMemory(address);

            _interface.WriteToPort(address, data);

            _state[RegisterPair.HL]--;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = true;
            _state[Flag.ParityOverflow] = _state[RegisterPair.BC] != 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] - 1);
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void OUTI()
    {
        unchecked
        {
            var address = _state[RegisterPair.HL];

            var data = _interface.ReadFromMemory(address);

            var port = _state[RegisterPair.BC];

            _interface.WriteToPort(port, data);

            _state[RegisterPair.HL]++;

            _state[Register.B]--;

            _state[Flag.Carry] = data > _state[Register.A];
            _state[Flag.AddSubtract] = (data & 0x80) > 0;
            _state[Flag.ParityOverflow] = _state[Register.B].IsEvenParity();
            _state[Flag.X1] = (_state[Register.B] & 0x08) > 0;
            _state[Flag.HalfCarry] = (_state[Register.A] & 0x0F) < (data & 0x0F);
            _state[Flag.X2] = (_state[Register.B] & 0x20) > 0;
            _state[Flag.Zero] = _state[Register.B] == 0;
            _state[Flag.Sign] = (sbyte) _state[Register.B] < 0;

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);
        }

        _state.SetMCycles(4, 5, 3, 4);
    }

    private void OUT_an_R(byte[] parameters, Register register)
    {
        unchecked
        {
            var address = (ushort) ((_state[register] << 8) | parameters[0]);

            _interface.WriteToPort(address, _state[register]);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 4);
    }

    private void OUT_C_R(Register register)
    {
        unchecked
        {
            _interface.WriteToPort(_state[RegisterPair.BC], _state[register]);

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4);
    }

    private void OUT_C_b(byte data)
    {
        unchecked
        {
            _interface.WriteToPort(_state[RegisterPair.BC], data);

            _state.MemPtr = (ushort) (_state[RegisterPair.BC] + 1);

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 4);
    }
}