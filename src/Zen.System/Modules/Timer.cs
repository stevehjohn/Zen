using System.Diagnostics;

namespace Zen.System.Modules;

public class Timer : IDisposable
{
    public required Func<int> OnTick { get; init; }

    public required Action HandleRefreshInterrupt { get; init; }

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly int _frameSleep;

    public bool Fast { get; set; }

    private bool _paused;

    public Timer(int framesPerSecond)
    {
        _frameSleep = 1_000 / framesPerSecond;

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;
    }

    public void Start()
    {
        Task.Run(TimerWorker, _cancellationToken);
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
        while (true)
        {
            if (! _paused)
            {
                var frameCycles = 0;

                while (frameCycles < 69_888)
                {
                    frameCycles += OnTick();
                }

                HandleRefreshInterrupt();
            }

            if (! Fast)
            {
                Thread.Sleep(_frameSleep);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}