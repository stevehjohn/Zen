// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void RET(Flag flag, bool not = false)
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

        _state.SetMCycles(5);
    }
}