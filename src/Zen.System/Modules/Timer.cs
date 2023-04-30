using System.Diagnostics;

namespace Zen.System.Modules;

public class Timer : IDisposable
{
    public required Func<int> OnTick { get; init; }

    public required Action HandleRefreshInterrupt { get; init; }

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly double _microsecondsPerCycle;

    public bool Fast { get; set; }

    private bool _paused;

    public Timer(double speedHz)
    {
        _microsecondsPerCycle = 1.0f / speedHz * Stopwatch.Frequency;

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;
    }

    public void Start()
    {
        Task.Run(TimerWorker, _cancellationToken);

        //Task.Run(InterruptWorker, _cancellationToken);
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        _paused = false;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();

        _cancellationTokenSource.Dispose();
    }

    private void TimerWorker()
    {
        var stopwatch = new Stopwatch();

        var cycles = 0;

        // TODO: Get this working properly.
        while (true)
        {
            if (! Fast)
            {
                stopwatch.Restart();

                while (stopwatch.ElapsedTicks < cycles * _microsecondsPerCycle)
                {
                }
            }

            if (! _paused)
            {
                cycles = OnTick();
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void InterruptWorker()
    {
        var stopwatch = new Stopwatch();

        while (true)
        {
            stopwatch.Restart();

            while (stopwatch.ElapsedMilliseconds < 20)
            {
            }

            if (! _paused)
            {
                HandleRefreshInterrupt();
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}