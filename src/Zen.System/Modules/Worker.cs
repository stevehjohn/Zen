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

    private readonly (int Address, byte Data)[] _vramChanges = new (int, byte)[2];

    public Worker(Interface @interface, VideoAdapter videoAdapter, int framesPerSecond)
    {
        _interface = @interface;

        _videoAdapter = videoAdapter;

        _frameSleep = 1_000 / framesPerSecond;

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;

        _vramChanges[0].Address = -1;
        _vramChanges[1].Address = -1;
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

    public void VRamUpdated(int address, byte data)
    {
        var i = 1;

        if (_vramChanges[1].Address != -1)
        {
            i = 0;
        }

        _vramChanges[i].Address = address;
        _vramChanges[i].Data = data;
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

                        if (i < 6 && cycles[i + 1] == 0 && _vramChanges[1].Address != -1)
                        {
                            _videoAdapter.ApplyRamChange(_vramChanges[1].Address, _vramChanges[1].Data);
                        }

                        if (i < 5 && cycles[i + 2] == 0 && _vramChanges[0].Address != -1)
                        {
                            _videoAdapter.ApplyRamChange(_vramChanges[0].Address, _vramChanges[0].Data);
                        }

                        if (cycles[i] > 0)
                        {
                            _videoAdapter.CycleComplete(frameCycles);
                        }
                    }

                    _vramChanges[0].Address = -1;
                    _vramChanges[1].Address = -1;
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