using Zen.Z80.Processor;

namespace Zen.Z80.Interfaces;

public interface IProcessorHook
{
    bool Activate(State state);

    bool ExecuteCycle(State state, Interface @interface);

    void PassiveCycle(State state, Interface @interface);
}