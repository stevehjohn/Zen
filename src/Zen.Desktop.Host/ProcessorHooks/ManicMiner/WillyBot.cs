using System.Collections.Generic;
using System.Linq;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class WillyBot : IProcessorHook
{
    private int _level;

    private int _cycle;

    private Move _move = Move.None;

    private Move _lastMove;
    
    private int _moveCycle;

    private int[,] _visitCount = new int[256, 192];

    private readonly HashSet<(Move, int)> _dangerMoves = [];
    
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
                _dangerMoves.Add((_lastMove, _moveCycle));
                
                _visitCount = new int[256, 192];
                
                _cycle = 0;

                _move = Move.None;
                
                break;
            
            case 0x9028:
                // Level complete
                _level++;

                _cycle = 0;

                _visitCount = new int[256, 192];
                
                _dangerMoves.Clear();

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
                // Jump
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

                _cycle = 0;

                _visitCount = new int[256, 192];
                
                _dangerMoves.Clear();

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

        possible = possible.OrderBy(p => p.Count).ToList();

        while (_dangerMoves.Contains((possible.First().Move, _cycle)))
        {
            possible.RemoveAt(0);
        }

        _move = possible.First().Move;

        _lastMove = _move;
        
        _moveCycle = _cycle;
        
        switch (_move)
        {
            case Move.Left:
                _visitCount[x - 2, y]++;
                break;
            
            case Move.Right:
                _visitCount[x + 2, y]++;
                break;
            
            case Move.UpLeft:
                _visitCount[x - 2, y - 4]++;
                break;
            
            case Move.UpRight:
                _visitCount[x + 2, y - 4]++;
                break;
            
            case Move.Up:
                _visitCount[x, y - 4]++;
                break;
        }
    }
}