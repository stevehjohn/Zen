using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class RoutePlanner
{
    private readonly Interface _interface;

    private readonly LevelData _levelData;

    public RoutePlanner(int level, Interface @interface)
    {
        _interface = @interface;

        _levelData = new LevelData(level, @interface);
    }

    public List<Move> GetRoute()
    {
        return null;
    }
}