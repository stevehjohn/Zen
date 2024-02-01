using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class WillyBot : IProcessorHook
{
    private int _level;

    private RoutePlanner _routePlanner;
    
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
        if (_level > 20)
        {
            return;
        }

        switch (state.ProgramCounter)
        {
            case 0x8D07:
            case 0x88FF:
                // Dead

                RestartLevel(@interface);
                
                break;
            
            case 0x9028:
                // Level complete
                _level++;

                StartLevel(@interface);

                break;
            
            case 0x85CC:
                // Title screen
                
                break;
            
            case 0x8C2F:
                // Left, right

                break;

            case 0x8C77:
                // Jump, called after left, right in a given cycle

                break;

            case 0x9312:
                // Start game
                _level = 1;

                StartLevel(@interface);
                
                state[Flag.Zero] = false;

                break;
            
            case 0x8938:
                // Infinite lives
                @interface.WriteToMemory(0x8457, 3);
                
                break;
            
            case 0x870E:
                // Main loop
                var cell = @interface.ReadFromMemory(0x806C) + (@interface.ReadFromMemory(0x806D) << 8) - 0x5C00;

                var y = @interface.ReadFromMemory(0x8068) / 2;

                var x = cell % 32 * 8;

                var frame = @interface.ReadFromMemory(0x8069);

                x += frame * 2;

                var grounded = @interface.ReadFromMemory(0x806B) == 0;
                
                break;
        }
    }

    private void StartLevel(Interface @interface)
    {
        _routePlanner = new RoutePlanner(_level, @interface);
        
        _routePlanner.GetRoutes();
        
        RestartLevel(@interface);
    }

    private void RestartLevel(Interface @interface)
    {
    }
}