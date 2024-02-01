using System.Collections.Generic;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class WillyBot : IProcessorHook
{
    private int _level;

    private RoutePlanner _routePlanner;

    private Queue<Move> _route;

    private Move _move;
    
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

                RestartLevel();
                
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
                if (_move == Move.Left || _move == Move.UpLeft)
                {
                    state[Register.A] = 2;
                    
                    return;
                }
                
                if (_move == Move.Right || _move == Move.UpRight)
                {
                    state[Register.A] = 1;
                    
                    return;
                }

                state[Register.A] = 0;

                break;

            case 0x8C77:
                // Jump, called after left, right in a given cycle
                if (_move == Move.Up || _move == Move.UpLeft || _move == Move.UpRight)
                {
                    state[Register.A] = 16;
                }
                else
                {
                    state[Register.A] = 0;
                }

                _move = Move.None;

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

                if (grounded)
                {
                    _move = _route.Dequeue();
                }

                break;
        }
    }

    private void StartLevel(Interface @interface)
    {
        _routePlanner = new RoutePlanner(_level, @interface);
        
        _route.Clear();
        
        _routePlanner.GetRoute().ForEach(m => _route.Enqueue(m));
        
        RestartLevel();
    }

    private void RestartLevel()
    {
        _route.Clear();
        
        _routePlanner.GetRoute().ForEach(m => _route.Enqueue(m));
    }
}