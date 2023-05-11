using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.System.ProcessorHooks;

public class LdBytesHook : IProcessorHook
{
    public bool Activate(State state)
    {
        return state.ProgramCounter == 0x0556;
    }

    public bool ExecuteCycle(State state, Interface @interface)
    {
        Ret(state, @interface);

        return true;
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