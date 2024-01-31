using System;
using System.Collections.Generic;
using System.Linq;
using Zen.Common;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class WillyBot : IProcessorHook
{
    private int _level;

    private int _cycle;

    private Move _move = Move.None;

    private List<(Move Move, int Cycle)> _moves;
    
    private HashSet<(Move Move, int Cycle)> _dangerMoves;

    private int[,] _visitCounts;
    
    private readonly MapCell[,] _map = new MapCell[32, 16];

    private readonly List<(int X, int Y)> _keys = [];
    
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
                if (_moves.Count > 0)
                {
                    _dangerMoves.Add((_moves.Last().Move, _moves.Last().Cycle));
                }

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
                
                _visitCounts[x, y]++;
                
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
        _visitCounts[x, y]++;
        
        var moves = new List<(Move Move, int Heuristic)>();

        if (x > 8)
        {
            moves.Add((Move.Left, CalculateHeuristic(x - 2, y) * 2));
            moves.Add((Move.UpLeft, CalculateHeuristic(x - 2, y - 4)));
        }

        if (x < 238)
        {
            moves.Add((Move.Right, CalculateHeuristic(x + 2, y) * 2));
            moves.Add((Move.UpRight, CalculateHeuristic(x + 2, y - 4)));
        }
        
        moves.Add((Move.Up, CalculateHeuristic(x, y - 4)));
        
        moves = moves.OrderBy(m => m.Heuristic).ToList();

        // foreach (var move in moves)
        // {
        //     Console.WriteLine($"{move.Move}: {move.Heuristic}");
        // }
        //
        // Console.WriteLine();
        
        while (_dangerMoves.Contains((moves.First().Move, _cycle)))
        {
            moves.RemoveAt(0);
        }

        _move = moves.First().Move;
        
        switch (_move)
        {
            case Move.Left:
                _visitCounts[x - 2, y]++;
                break;
            
            case Move.Right:
                _visitCounts[x + 2, y]++;
                break;
            
            case Move.UpLeft:
                _visitCounts[x - 2, y - 4]++;
                break;
            
            case Move.UpRight:
                _visitCounts[x + 2, y - 4]++;
                break;
            
            case Move.Up:
                _visitCounts[x, y - 4]++;
                break;
        }
        
        _moves.Add((_move, _cycle));
    }

    private int CalculateHeuristic(int x, int y)
    {
        var value = 0;
        
        foreach (var key in _keys)
        {
            value += Math.Abs(x - key.X) + Math.Abs(y - key.Y) * (_visitCounts[x, y] + 1);
        }

        return value;
    }

    private void StartLevel(Interface @interface)
    {
        _dangerMoves = [];

        ParseMap(@interface);
        
        RestartLevel();
    }

    private void RestartLevel()
    {
        _moves = [];

        _visitCounts = new int[Constants.PaperWidthPixels, Constants.PaperHeightPixels];
        
        _cycle = 0;
    }

    private void ParseMap(Interface @interface)
    {
        var start = 0xA000 + 0x1000 * _level;

        for (var y = 0; y < 16; y++)
        {
            for (var x = 0; x < 32; x++)
            {
                var tile = ParseTile(@interface, (ushort) (start + y * 32 + x));
                
                _map[x, y] = tile;
            }
        }
        
        // _keys.Add((9 * 8, 0));
        // _keys.Add((29 * 8, 0));
        // _keys.Add((16 * 8, 1 * 8));
        // _keys.Add((24 * 8, 4 * 8));
        _keys.Add((30 * 8, 6 * 8));
    }

    private static MapCell ParseTile(Interface @interface, ushort location)
    {
        var data = @interface.ReadFromMemory(location);

        return data switch
        {
            0x42 => MapCell.Floor,
            0x02 => MapCell.Floor,
            0x16 => MapCell.Wall,
            0x04 => MapCell.Floor,
            0x44 => MapCell.Hazard,
            0x05 => MapCell.Hazard,
            _ => MapCell.Empty
        };
    }
}