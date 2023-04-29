// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void CALL_nn(byte[] parameters)
    {
        unchecked
        {
            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) ((_state.ProgramCounter & 0xFF00) >> 8));

            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) (_state.ProgramCounter & 0x00FF));

            _state.ProgramCounter = (ushort) ((parameters[1] << 8) | parameters[0]);

            _state.MemPtr = (ushort) (parameters[1] << 8 | parameters[0]);
        }

        _state.SetMCycles(4, 3, 4, 3, 3);
    }

    private void CALL_F_nn(Flag flag, byte[] parameters, bool not = false)
    {
        unchecked
        {
            var condition = not ? ! _state[flag] : _state[flag];

            if (condition)
            {
                _state.StackPointer--;

                _interface.WriteToMemory(_state.StackPointer, (byte) ((_state.ProgramCounter & 0xFF00) >> 8));

                _state.StackPointer--;

                _interface.WriteToMemory(_state.StackPointer, (byte) (_state.ProgramCounter & 0x00FF));

                _state.ProgramCounter = (ushort) ((parameters[1] << 8) | parameters[0]);

                _state.MemPtr = (ushort) (parameters[1] << 8 | parameters[0]);

                _state.Q = 0;

                _state.SetMCycles(4, 3, 4, 3, 3);

                return;
            }
        }

        _state.MemPtr = (ushort) (parameters[1] << 8 | parameters[0]);

        _state.Q = 0;

        _state.SetMCycles(4, 3, 3);
    }

    private void DJNZ_e(byte[] parameters)
    {
        unchecked
        {
            _state[Register.B]--;

            if (_state[Register.B] > 0)
            {
                _state.ProgramCounter += (ushort) (sbyte) parameters[0];

                _state.Q = 0;

                _state.SetMCycles(5, 3, 5);

                return;
            }
        }

        _state.Q = 0;

        _state.SetMCycles(5, 3);
    }

    private void JP_nn(byte[] parameters)
    {
        unchecked
        {
            _state.ProgramCounter = parameters.ReadLittleEndian();

            _state.MemPtr = _state.ProgramCounter;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3);
    }

    private void JP_F_nn(Flag flag, byte[] parameters, bool not = false)
    {
        unchecked
        {
            var condition = not ? ! _state[flag] : _state[flag];

            if (condition)
            {
                _state.ProgramCounter = parameters.ReadLittleEndian();

                _state.MemPtr = _state.ProgramCounter;
            }

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3);
    }

    private void JR_e(byte[] parameters)
    {
        unchecked
        {
            _state.ProgramCounter = (ushort) (_state.ProgramCounter + (sbyte) parameters[0]);

            _state.MemPtr = _state.ProgramCounter;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 5);
    }

    private void JR_F_e(Flag flag, byte[] parameters, bool not = false)
    {
        unchecked
        {
            var condition = not ? ! _state[flag] : _state[flag];

            if (condition)
            {
                _state.ProgramCounter = (ushort) (_state.ProgramCounter + (sbyte) parameters[0]);

                _state.MemPtr = _state.ProgramCounter;

                _state.Q = 0;

                _state.SetMCycles(4, 3, 5);

                return;
            }
        }

        _state.Q = 0;

        _state.SetMCycles(4, 3);
    }
    
    private void RET()
    {
        unchecked
        {
            var data = _interface.ReadFromMemory(_state.StackPointer);

            _state.ProgramCounter = data;

            _state.StackPointer++;

            data = _interface.ReadFromMemory(_state.StackPointer);

            _state.ProgramCounter = (ushort) ((_state.ProgramCounter & 0x00FF) | data << 8);

            _state.StackPointer++;

            _state.MemPtr = _state.ProgramCounter;
        }

        _state.Q = 0;

        _state.SetMCycles(4, 3, 3);
    }

    private void RET_F(Flag flag, bool not = false)
    {
        unchecked
        {
            var condition = not ? ! _state[flag] : _state[flag];

            if (condition)
            {
                var data = _interface.ReadFromMemory(_state.StackPointer);

                _state.ProgramCounter = data;

                _state.StackPointer++;

                data = _interface.ReadFromMemory(_state.StackPointer);

                _state.ProgramCounter = (ushort) ((_state.ProgramCounter & 0x00FF) | data << 8);

                _state.StackPointer++;

                _state.MemPtr = _state.ProgramCounter;

                _state.Q = 0;

                _state.SetMCycles(5, 3, 3);

                return;
            }

            _state.Q = 0;
        }

        _state.SetMCycles(5);
    }

    private void RST(byte address)
    {
        unchecked
        {
            var pc = _state.ProgramCounter;

            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) ((pc & 0xFF00) >> 8));

            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) (pc & 0x00FF));

            _state.ProgramCounter = address;

            _state.MemPtr = _state.ProgramCounter;

            _state.Q = 0;
        }

        _state.SetMCycles(5, 3, 3);
    }
}