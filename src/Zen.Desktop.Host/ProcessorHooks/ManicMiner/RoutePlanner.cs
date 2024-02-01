using System;
using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class RoutePlanner
{
    private readonly LevelData _levelData;

    private readonly PriorityQueue<(int X, int Y, List<Move> Moves, HashSet<(int X, int Y)> Keys, HashSet<(int X, int Y)> Visited, int Steps), int> _queue = new();
    
    public RoutePlanner(int level, Interface @interface)
    {
        _levelData = new LevelData(level, @interface);
    }

    public void Initialise()
    {
        _queue.Enqueue((_levelData.Start.X * 8, _levelData.Start.Y * 8, [], [], [], 0), 0);
    }

    public List<Move> GetNextRoute()
    {
        while (_queue.TryDequeue(out var node, out _))
        {
            if (IsComplete(node))
            {
                return node.Moves;
            }

            var key = CheckKey(node.X, node.Y);

            if (key.X != -1)
            {
                node.Keys.Add((key.X, key.Y));
                
                Console.WriteLine($"{node.Keys.Count}");
            }

            var moves = GetMoves(node.X, node.Y);
            
            foreach (var move in moves)
            {
                _queue.Enqueue((move.X, move.Y, [..node.Moves, move.Move], node.Keys, [..node.Visited, (move.X, move.Y)], node.Steps + 1), node.Steps + 1);
            }
        }

        return null;
    }

    private (int X, int Y) CheckKey(int x, int y)
    {
        var cellX = x / 8;

        var cellY = y / 8;

        if (_levelData.Keys.Contains((cellX, cellY)))
        {
            return (cellX, cellY);
        }

        if (_levelData.Keys.Contains((cellX, cellY + 1)))
        {
            return (cellX, cellY);
        }

        if (x % 8 != 0)
        {
            if (_levelData.Keys.Contains((cellX + 1, cellY)))
            {
                return (cellX + 1, cellY);
            }

            if (_levelData.Keys.Contains((cellX + 1, cellY + 1)))
            {
                return (cellX + 1, cellY);
            }
        }

        return (-1, -1);
    }

    private bool IsComplete((int X, int Y, List<Move> Moves, HashSet<(int X, int Y)> Keys, HashSet<(int X, int Y)> Visited, int Steps) node)
    {
        if (node.Keys.Count < _levelData.Keys.Count)
        {
            return false;
        }

        var x = node.X / 8;

        var y = node.Y / 8;

        if (x >= _levelData.End.X && x <= _levelData.End.X + 1 && y >= _levelData.End.Y && y <= _levelData.End.Y + 1)
        {
            return true;
        }

        return false;
    }

    private List<(Move Move, int X, int Y)> GetMoves(int x, int y)
    {
        var moves = new List<(Move, int X, int Y)>();

        if (TryWalk(x, y, -2))
        {
            moves.Add((Move.Left, x - 2, y));
        }

        if (TryWalk(x, y, 2))
        {
            moves.Add((Move.Left, x + 2, y));
        }

        var jump = TryJump(x, y, -2);

        if (jump.Safe)
        {
            moves.Add((Move.UpLeft, jump.X, jump.Y));
        }

        jump = TryJump(x, y, 2);

        if (jump.Safe)
        {
            moves.Add((Move.UpLeft, jump.X, jump.Y));
        }

        return moves;
    }

    private bool TryWalk(int x, int y, int dX)
    {
        x += dX;
        
        if (x < 8 || x > 238)
        {
            return false;
        }

        return CheckSafe(x, y) && CheckOpen(x, y);
    }

    private (bool Safe, int X, int Y) TryJump(int x, int y, int dX)
    {
        var velocities = new[] {-4, -4, -3, -3, -2, -2, -1, -1, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4};
        
        for (var i = 0; i < velocities.Length; i++)
        {
            var velocity = velocities[i];
            
            if (! CheckSafe(x + dX, y + velocity))
            {
                return (false, -1, -1);
            }

            if (CheckOpen(x + dX, y + velocity))
            {
                y += velocity;

                if (i < velocities.Length - 1)
                {
                    x += dX;
                }
            }

            if (velocity < 0 && CheckLanded(x, y))
            {
                return (true, x, y);
            }
        }

        while (! CheckLanded(x, y))
        {
            if (! CheckSafe(x, y))
            {
                return (false, -1, -1);
            }

            y += 4;
        }

        return (true, x, y);
    }

    private bool CheckLanded(int x, int y)
    {
        var cell = _levelData.Map[x / 8, y / 8 + 1];

        if (cell == MapCell.Floor || cell == MapCell.Wall)
        {
            return true;
        }

        if (x % 8 != 0)
        {
            cell = _levelData.Map[x / 8 + 1, y / 8 + 1];

            if (cell == MapCell.Floor || cell == MapCell.Wall)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckSafe(int x, int y)
    {
        var cell = _levelData.Map[x / 8, y / 8];

        if (cell == MapCell.Hazard)
        {
            return false;
        }

        cell = _levelData.Map[x / 8, y / 8 + 1];

        if (cell == MapCell.Hazard)
        {
            return false;
        }

        if (x % 8 != 0)
        {
            cell = _levelData.Map[x / 8 + 1, y / 8];

            if (cell == MapCell.Hazard)
            {
                return false;
            }

            cell = _levelData.Map[x / 8 + 1, y / 8 + 1];

            if (cell == MapCell.Hazard)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckOpen(int x, int y)
    {
        var cell = _levelData.Map[x / 8, y / 8];

        if (cell == MapCell.Wall)
        {
            return false;
        }

        cell = _levelData.Map[x / 8, y / 8 + 1];

        if (cell == MapCell.Wall)
        {
            return false;
        }

        return true;
    }
}