using System.Diagnostics;

namespace Zen.Common.Infrastructure;

public class Counters
{
    private readonly long[] _totalCounts;

    private readonly long[] _temporaryCountsPerSecond;

    private readonly long[] _countsPerSecond;

    private readonly long[] _lastResetMilliseconds;

    private readonly Stopwatch _stopwatch;

    public static Counters Instance { get; } = new();

    private Counters()
    {
        var numberOfCounters = Enum.GetNames<Counter>().Length;

        _totalCounts = new long[numberOfCounters];

        _temporaryCountsPerSecond = new long[numberOfCounters];

        _countsPerSecond = new long[numberOfCounters];

        _lastResetMilliseconds = new long[numberOfCounters];

        _stopwatch = Stopwatch.StartNew();
    }

    public void IncrementCounter(Counter counter)
    {
        _totalCounts[(int) counter]++;

        if (_stopwatch.ElapsedMilliseconds - _lastResetMilliseconds[(int) counter] > 1000)
        {
            _lastResetMilliseconds[(int) counter] = _stopwatch.ElapsedMilliseconds;

            _countsPerSecond[(int) counter] = _temporaryCountsPerSecond[(int) counter];

            _temporaryCountsPerSecond[(int) counter] = 1;
        }
        else
        {
            _temporaryCountsPerSecond[(int) counter]++;
        }
    }

    public long GetCountPerSecond(Counter counter)
    {
        return _countsPerSecond[(int) counter];
    }
}