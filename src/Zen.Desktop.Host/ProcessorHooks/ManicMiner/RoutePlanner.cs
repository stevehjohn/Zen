using System;
using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class RoutePlanner
{
    private readonly LevelData _levelData;

    private readonly PriorityQueue<(int X, int Y, List<Move> Moves, int KeyMask, int Cost), int> _queue = new();

    private readonly Dictionary<(int X, int Y), int> _keyBits = new();

    private readonly Dictionary<(int X, int Y, int KeyMask), int> _bestCosts = new();

    private readonly HashSet<(int X, int Y)> _deathCells;

    private readonly int _allKeysMask;

    public RoutePlanner(int level, Interface @interface, IEnumerable<(int X, int Y)> deathCells = null)
    {
        _levelData = new LevelData(level, @interface);
        _deathCells = deathCells == null ? [] : [..deathCells];

        for (var i = 0; i < _levelData.Keys.Count; i++)
        {
            _keyBits[_levelData.Keys[i]] = 1 << i;
        }

        _allKeysMask = (1 << _levelData.Keys.Count) - 1;
    }

    public void Initialise()
    {
        var startX = _levelData.Start.X * 8;
        var startY = _levelData.Start.Y * 8;

        _queue.Enqueue((startX, startY, [], 0, 0), 0);
        _bestCosts[(startX, startY, 0)] = 0;
    }

    public List<Move> GetNextRoute()
    {
        while (_queue.TryDequeue(out var node, out _))
        {
            var nodeState = (node.X, node.Y, node.KeyMask);

            if (_bestCosts.TryGetValue(nodeState, out var bestCost) && node.Cost > bestCost)
            {
                continue;
            }

            var keyMask = AddKey(node.KeyMask, node.X, node.Y);

            if (IsComplete(node.X, node.Y, keyMask))
            {
                return node.Moves;
            }

            var moves = GetMoves(node.X, node.Y);

            foreach (var move in moves)
            {
                var penalty = GetDeathPenalty(move.X, move.Y);
                var cost = node.Cost + 1 + penalty;
                var nextState = (move.X, move.Y, keyMask);

                if (_bestCosts.TryGetValue(nextState, out bestCost) && bestCost <= cost)
                {
                    continue;
                }

                _bestCosts[nextState] = cost;

                _queue.Enqueue((move.X, move.Y, [..node.Moves, move.Move], keyMask, cost), cost);
            }
        }

        return null;
    }

    private int AddKey(int keyMask, int x, int y)
    {
        var key = CheckKey(x, y);

        if (key.X == -1)
        {
            return keyMask;
        }

        if (_keyBits.TryGetValue((key.X, key.Y), out var bit))
        {
            return keyMask | bit;
        }

        return keyMask;
    }

    private (int X, int Y) CheckKey(int x, int y)
    {
        var cellX = x / 8;

        var cellY = y / 8;

        if (_keyBits.ContainsKey((cellX, cellY)))
        {
            return (cellX, cellY);
        }

        if (_keyBits.ContainsKey((cellX, cellY + 1)))
        {
            return (cellX, cellY + 1);
        }

        if (x % 8 != 0)
        {
            if (_keyBits.ContainsKey((cellX + 1, cellY)))
            {
                return (cellX + 1, cellY);
            }

            if (_keyBits.ContainsKey((cellX + 1, cellY + 1)))
            {
                return (cellX + 1, cellY + 1);
            }
        }

        return (-1, -1);
    }

    private bool IsComplete(int x, int y, int keyMask)
    {
        if (keyMask != _allKeysMask)
        {
            return false;
        }

        var cellX = x / 8;

        var cellY = y / 8;

        return cellX >= _levelData.End.X && cellX <= _levelData.End.X + 1 &&
               cellY >= _levelData.End.Y && cellY <= _levelData.End.Y + 1;
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
            moves.Add((Move.Right, x + 2, y));
        }

        var jump = TryJump(x, y, -2);

        if (jump.Safe)
        {
            moves.Add((Move.UpLeft, jump.X, jump.Y));
        }

        jump = TryJump(x, y, 2);

        if (jump.Safe)
        {
            moves.Add((Move.UpRight, jump.X, jump.Y));
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

            if (!CheckSafe(x + dX, y + velocity))
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

        while (!CheckLanded(x, y))
        {
            if (!CheckSafe(x, y))
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

    private int GetDeathPenalty(int x, int y)
    {
        if (_deathCells.Count == 0)
        {
            return 0;
        }

        var cell = (x / 8, y / 8);

        if (_deathCells.Contains(cell))
        {
            return 10000;
        }

        foreach (var deathCell in _deathCells)
        {
            var distance = Math.Abs(deathCell.X - cell.Item1) + Math.Abs(deathCell.Y - cell.Item2);

            if (distance <= 2)
            {
                return 1000;
            }

            if (distance <= 4)
            {
                return 250;
            }
        }

        return 0;
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
