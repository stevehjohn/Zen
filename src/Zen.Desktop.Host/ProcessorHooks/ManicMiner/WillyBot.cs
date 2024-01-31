using System.Collections.Generic;
using System.Linq;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class WillyBot : IProcessorHook
{
    private int _level;

    private int _cycle;

    private byte _direction;

    private bool _jump;

    private int[,] _visitCount = new int[256, 192];
    
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
                _cycle = 0;
                
                break;
            
            case 0x9028:
                // Level complete
                _level++;

                _cycle = 0;

                _visitCount = new int[256, 192];

                break;
            
            case 0x85CC:
                // Title screen
                
                break;
            
            case 0x8C2F:
                // Left, right
                state[Register.A] = _direction;
                
                break;
            
            case 0x8C77:
                // Jump
                state[Register.A] = (byte) (_jump ? 16 : 0);
                
                break;
            
            case 0x9312:
                // Start game
                _level = 1;

                _cycle = 0;

                _visitCount = new int[256, 192];

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
                    GenerateNextMove(x, y);
                }

                _cycle++;

                break;
        }
    }

    private void GenerateNextMove(int x, int y)
    {
        _visitCount[x, y]++;
        
        var possible = new List<(Move Move, int Count)>();

        if (x > 8)
        {
            possible.Add((Move.Left, _visitCount[x - 2, y]));
            possible.Add((Move.UpLeft, _visitCount[x - 2, y - 4]));
        }

        if (x < 238)
        {
            possible.Add((Move.Right, _visitCount[x + 2, y]));
            possible.Add((Move.UpRight, _visitCount[x + 2, y - 4]));
        }
        
        possible.Add((Move.Up, _visitCount[x, y - 4]));

        var move = possible.MinBy(p => p.Count).Move;

        switch (move)
        {
            case Move.Left:
                _direction = 2;
                _jump = false;
                break;
            
            case Move.Right:
                _direction = 1;
                _jump = false;
                break;
            
            case Move.UpLeft:
                _direction = 2;
                _jump = true;
                break;
            
            case Move.UpRight:
                _direction = 1;
                _jump = true;
                break;
            
            case Move.Up:
                _direction = 0;
                _jump = true;
                break;
        }
    }
}