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
        var queue = new PriorityQueue<(int X, int Y), int>();
        
        queue.Enqueue((_levelData.Start.X * 8, _levelData.Start.Y * 8), 0);

        while (queue.TryDequeue(out var position, out _))
        {
        }

        return null;
    }
}