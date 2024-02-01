using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class RoutePlanner
{
    private readonly LevelData _levelData;

    public RoutePlanner(int level, Interface @interface)
    {
        _levelData = new LevelData(level, @interface);
    }

    public List<Move> GetRoute()
    {
        var queue = new PriorityQueue<(int X, int Y, List<Move> Moves, int Steps), int>();
        
        queue.Enqueue((_levelData.Start.X * 8, _levelData.Start.Y * 8, [], 0), 0);

        while (queue.TryDequeue(out var node, out _))
        {
            var moves = GetMoves(node.X, node.Y);

            foreach (var move in moves)
            {
                queue.Enqueue((move.X, move.Y, [..node.Moves, move.Move], node.Steps + 1), node.Steps + 1);
            }
        }

        return null;
    }

    private List<(Move Move, int X, int Y)> GetMoves(int x, int y)
    {
        return null;
    }

    private bool IsMoveSafe(int x, int y)
    {
        return false;
    }
}