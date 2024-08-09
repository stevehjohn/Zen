using Zen.Common;
using Zen.Common.Infrastructure;
using Zen.Z80.Processor;

// ReSharper disable IdentifierTypo

namespace Zen.System.Modules;

public class Worker : IDisposable
{
    public required Func<int, byte[]> OnTick { get; init; }

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly Interface _interface;

    private readonly VideoModulator _videoModulator;

    private readonly AyAudio _ayAudio;

    private readonly (int Address, byte Data)[] _vramChanges = new (int, byte)[2];

    private readonly ManualResetEvent _resetEvent = new(true);

    private readonly AutoResetEvent _scanResetEvent = new(true);
    
    private bool _paused;

    private Task? _workerThread;

    private int _scanStates;
    
    public bool Fast { get; set; }
    
    public bool Slow { get; set; }

    public Worker(Interface @interface, VideoModulator videoModulator, AyAudio ayAudio)
    {
        _interface = @interface;

        _videoModulator = videoModulator;

        _ayAudio = ayAudio;

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;

        _vramChanges[0].Address = -1;
        _vramChanges[1].Address = -1;
    }

    public void Start()
    {
        _workerThread = Task.Run(TimerWorker, _cancellationToken);
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        _paused = false;
    }

    public void ScanComplete()
    {
        _scanResetEvent.Set();
    }

    public void Dispose()
    {
        if (_workerThread == null)
        {
            _cancellationTokenSource.Dispose();

            return;
        }

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
        while (! _cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (! _paused)
                {
                    var frameCycles = 0;

                    _videoModulator.StartFrame();

                    while (frameCycles < Constants.FrameCycles)
                    {
                        _interface.INT = frameCycles is >= Constants.InterruptStart and < Constants.InterruptEnd;

                        ClearFrameRamBuffer();

                        var cycles = OnTick(frameCycles);

                        var instructionCycles = 0;
                        
                        for (var i = 0; i < 7; i++)
                        {
                            if (i > 0 && cycles[i] == 0)
                            {
                                break;
                            }

                            frameCycles += cycles[i];

                            instructionCycles += cycles[i];

                            frameCycles += ApplyFrameRamChanges(i, frameCycles, cycles);

                            if (cycles[i] > 0)
                            {
                                _videoModulator.CycleComplete(frameCycles);
                            }
                        }

                        _scanStates += instructionCycles;

                        if (Slow && _scanStates > Constants.WorkerScanlinePause)
                        {
                            _scanResetEvent.WaitOne();
                        }

                        if (_scanStates > Constants.WorkerScanlinePause)
                        {
                            _scanStates -= Constants.WorkerScanlinePause;
                        }
                    }

                    if (! Fast)
                    {
                        _resetEvent.WaitOne();
                    }

                    _ayAudio.FrameReady(_resetEvent);

                    _resetEvent.Reset();

                    Counters.Instance.IncrementCounter(Counter.SpectrumFrames);
                }
            }
            catch (Exception exception)
            {
                Logger.LogException(nameof(Worker), exception);

                throw;
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void ClearFrameRamBuffer()
    {
        _vramChanges[0].Address = -1;
        _vramChanges[1].Address = -1;
    }

    private int ApplyFrameRamChanges(int mcycle, int frameCycles, byte[] opCycles)
    {
        if (mcycle < 6 && opCycles[mcycle + 1] == 0 && _vramChanges[1].Address != -1)
        {
            _videoModulator.ApplyRamChange(_vramChanges[1].Address, _vramChanges[1].Data);

            return GetContention(frameCycles);
        }

        if (mcycle < 5 && opCycles[mcycle + 2] == 0 && _vramChanges[0].Address != -1)
        {
            _videoModulator.ApplyRamChange(_vramChanges[0].Address, _vramChanges[0].Data);

            return GetContention(frameCycles);
        }

        return 0;
    }

    private static int GetContention(int cycle)
    {
        if (cycle < 14_335 || cycle > 57_343)
        {
            return 0;
        }

        var start = cycle - 14_335;

        var linePosition = start % 224;

        if (linePosition > 128)
        {
            return 0;
        }

        var delay = 6 - linePosition % 8;

        if (delay < 0)
        {
            delay = 0;
        }

        return delay;
    }
}