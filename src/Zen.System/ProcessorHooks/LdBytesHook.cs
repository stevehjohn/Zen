using Zen.System.FileHandling;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.System.ProcessorHooks;

public class LdBytesHook : IProcessorHook
{
    private byte[] _data = Array.Empty<byte>();

    private int _position;

    private int _bit;

    private readonly TapFileLoader _loader;

    public LdBytesHook()
    {
        _loader = new TapFileLoader();
    }

    public void StageFile(string filename)
    {
        _loader.StageFile(filename);
    }

    public bool Activate(State state)
    {
        if (state.ProgramCounter == 0x0556 || state.ProgramCounter == 0x0562)
        {
            //_data = _loader.ReadNextBlock(state[Register.A] == 0x00);
            _data = _loader.ReadNextBlock();

            _position = 0;

            _bit = 0b0000_0001;

            return true;
        }

        return false;
    }

    public bool ExecuteCycle(State state, Interface @interface)
    {
        _bit <<= 1;

        if (_bit == 0x0100)
        {
            @interface.WriteToMemory(state[RegisterPair.IX], _data[_position]);

            state[RegisterPair.IX]++;

            state[RegisterPair.DE]--;

            state[Register.F] = 0x93;

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

    public void PassiveCycle(State state, Interface @interface)
    {
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