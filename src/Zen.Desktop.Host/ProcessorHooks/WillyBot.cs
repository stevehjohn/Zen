using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks;

public class WillyBot : IProcessorHook
{
    private int _level;

    private int _iteration;

    private int _cycle;

    private byte _direction;

    private bool _jump;

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
            case 0x88FF:
                // Dead
                _iteration++;

                _cycle = 0;
                
                break;
            
            case 0x9028:
                // Level complete
                _level++;

                _iteration = 0;

                _cycle = 0;
                
                break;
            
            case 0x85CC:
                // Title screen
                
                break;
            
            case 0x8C2F:
                // Left, right
                //state[Register.A] = _direction;
                
                break;
            
            case 0x8C77:
                // Jump
                //state[Register.A] = (byte) (_jump ? 16 : 0);
                
                break;
            
            case 0x9312:
                // Start game
                _level = 1;

                _iteration = 0;

                _cycle = 0;

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

                GenerateNextMove(x, y);
                
                break;
        }
    }

    private void GenerateNextMove(int x, int y)
    {
        _jump = true;
        
        _direction = 1;
    }
}