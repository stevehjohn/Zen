using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.System.ProcessorHooks;

public class LdBytesHook : IProcessorHook
{
    private const int PauseLength = 30;

    private byte[] _data = Array.Empty<byte>();

    private int _position;

    private int _bit;

    private int _pause;

    public bool Activate(State state)
    {
        if (state.ProgramCounter == 0x0556)
        {
            _data = File.ReadAllBytes("C:\\Users\\steve\\Downloads\\Aqua Plane.tap");
            
            _data = _data[3..20];

            _position = 0;

            _bit = 0b0000_0001;

            _pause = 0;

            return true;
        }

        return false;
    }

    public bool ExecuteCycle(State state, Interface @interface)
    {
        if (_pause > 0)
        {
            _pause--;

            return false;
        }

        _pause = PauseLength;

        @interface.WriteToPort(0xFE, (byte) ((_data[_position] & _bit) > 0 ? 5 : 6));

        _bit <<= 1;

        if (_bit == 0x0100)
        {
            @interface.WriteToMemory(state[RegisterPair.IX], _data[_position]);

            state[RegisterPair.IX]++;

            state[RegisterPair.DE]--;

            if (state[RegisterPair.DE] == 0)
            {
                Ret(state, @interface);

                state[Flag.Carry] = true;

                @interface.WriteToPort(0xFE, 7);

                return true;
            }

            _bit = 1;

            _position++;
        }

        return false;
    }

    private static void Ret(State state, Interface @interface)
    {
        var data = @interface.ReadFromMemory(state.StackPointer);

        state.ProgramCounter = data;

        state.StackPointer++;

        data = @interface.ReadFromMemory(state.StackPointer);

        state.ProgramCounter = (ushort) ((state.ProgramCounter & 0x00FF) | data << 8);

        state.StackPointer++;

        state.MemPtr = state.ProgramCounter;
    }
}