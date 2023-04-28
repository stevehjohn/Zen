// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void PUSH(RegisterPair registers)
    {
        unchecked
        {
            var data = _state[registers];
            
            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) ((data & 0xFF00) >> 8));

            _state.StackPointer--;

            _interface.WriteToMemory(_state.StackPointer, (byte) (data & 0x00FF));

            _state.Q = 0;
        }

        _state.SetMCycles(5, 3, 3);
    }

    public void POP(RegisterPair registers)
    {
        unchecked
        {
            var data = (ushort) _interface.ReadFromMemory(_state.StackPointer);

            _state.StackPointer++;

            data |= (ushort) (_interface.ReadFromMemory(_state.StackPointer) << 8);

            _state.StackPointer++;

            _state[registers] = data;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 3, 3);
    }
}