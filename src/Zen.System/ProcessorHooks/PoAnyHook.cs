using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.System.ProcessorHooks;

public class PoAnyHook : IProcessorHook
{
    private const string LogFile = "output.log";

    private int _previousRow;

    public PoAnyHook()
    {
        File.AppendAllText(LogFile, Environment.NewLine);
        File.AppendAllText(LogFile, Environment.NewLine);
    }

    public bool Activate(State state)
    {
        return false;
    }

    public bool ExecuteCycle(State state, Interface @interface)
    {
        return true;
    }

    public void PassiveCycle(State state, Interface @interface)
    {
        if (state.ProgramCounter != 0x0B24)
        {
            return;
        }

        if (state[Register.B] != _previousRow)
        {
            File.AppendAllText(LogFile, Environment.NewLine);
        }

        File.AppendAllText(LogFile, ((char) state[Register.A]).ToString());

        _previousRow = state[Register.B];
    }
}