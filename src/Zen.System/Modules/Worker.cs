using Zen.Z80.Processor;

namespace Zen.System.Modules;

public class Worker : IDisposable
{
    public required Func<byte[]> OnTick { get; init; }

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly Interface _interface;

    private readonly VideoAdapter _videoAdapter;

    private readonly int _frameSleep;

    public bool Fast { get; set; }

    private bool _paused;

    public Worker(Interface @interface, VideoAdapter videoAdapter, int framesPerSecond)
    {
        _interface = @interface;

        _videoAdapter = videoAdapter;

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

                _videoAdapter.StartFrame();

                while (frameCycles < 69_888)
                {
                    _interface.INT = frameCycles is >= 24 and < 56;

                    var cycles = OnTick();

                    for (var i = 0; i < 7; i++)
                    {
                        if (i > 0 && cycles[i] == 0)
                        {
                            break;
                        }

                        frameCycles += cycles[i];

                        if (cycles[i] > 0)
                        {
                            _videoAdapter.CycleComplete(frameCycles);
                        }
                    }
                }
            }

            if (! Fast)
            {
                Thread.Sleep(_frameSleep);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}