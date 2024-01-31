using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks;

public class WillyBot : IProcessorHook
{
    private int _level;

    private int _iteration;
    
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
                // Dead
                
                break;
            
            case 0x9028:
                // Level complete
                _level++;

                _iteration = 0;
                
                break;
            
            case 0x85CC:
                // Title screen
                
                break;
            
            case 0x8C2F:
                // Left, right
                state[Register.A] = 1;
                
                break;
            
            case 0x8C77:
                // Jump
                state[Register.A] = 16;
                
                break;
            
            case 0x9312:
                // Start game
                _level = 1;

                _iteration = 0;

                state[Flag.Zero] = false;

                break;
            
            case 0x8938:
                // Infinite lives
                @interface.WriteToMemory(0x8457, 3);
                
                break;
            
            case 0x870E:
                // Main loop
                GenerateNextMove();
                
                break;
        }
    }

    private void GenerateNextMove()
    {
    }
}