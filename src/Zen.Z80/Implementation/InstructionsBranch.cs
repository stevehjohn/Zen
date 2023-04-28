// ReSharper disable InconsistentNaming

using Zen.Common.Extensions;
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
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

                //_state.ProgramCounter--;

                _state.MemPtr = _state.ProgramCounter;

                _state.Q = 0;

                _state.SetMCycles(5, 3, 3);

                return;
            }

            _state.Q = 0;
        }

        _state.SetMCycles(5);
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

                _state.Q = 0;
            }
        }

        _state.SetMCycles(4, 3, 3);
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