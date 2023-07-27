using System.Diagnostics;

namespace Zen.Common.Infrastructure;

public class Counters
{
    private readonly ulong[] _totalCounts;

    private readonly ulong[] _temporaryCountsPerSecond;

    private readonly ulong[] _countsPerSecond;

    private readonly Stopwatch _stopwatch;

    public static Counters Instance { get; } = new ();

    private Counters()
    {
        var numberOfCounters = Enum.GetNames<Counter>().Length;

        _totalCounts = new ulong[numberOfCounters];

        _temporaryCountsPerSecond = new ulong[numberOfCounters];

        _countsPerSecond = new ulong[numberOfCounters];

        _stopwatch = Stopwatch.StartNew();
    }

    public void IncrementCounter(Counter counter)
    {
        _totalCounts[(int) counter]++;

        if (_stopwatch.ElapsedMilliseconds > 1000)
        {
            _stopwatch.Restart();

            _countsPerSecond[(int) counter] = _temporaryCountsPerSecond[(int) counter];

            _temporaryCountsPerSecond[(int) counter] = 1;
        }
        else
        {
            _temporaryCountsPerSecond[(int) counter]++;
        }
    }

    public ulong GetCounterPerSecond(Counter counter)
    {
        return _countsPerSecond[(int) counter];
    }
}