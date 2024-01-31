using System;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;
using NotImplementedException = System.NotImplementedException;

namespace Zen.Desktop.Host.ProcessorHooks;

public class WillyBot : IProcessorHook
{
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
        switch (state.ProgramCounter)
        {
            case 0x8D07:
                Console.WriteLine("Dead");
                
                break;
            
            case 0x9028:
                Console.WriteLine("Level Complete");
                
                break;
            
            case 0x85CC:
                Console.WriteLine("Title Screen");
                
                break;
        }
    }
}