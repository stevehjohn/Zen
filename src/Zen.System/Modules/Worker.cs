using Zen.Common;
using Zen.Common.Infrastructure;
using Zen.System.Infrastructure;
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

    private readonly ManualResetEvent _resetEvent = new(true);

    private readonly AutoResetEvent _scanResetEvent = new(true);
    
    private bool _paused;

    private Task _workerThread;

    private int _scanStates;

    private int _lastScanComplete;

    private int _frameCycles;

    private readonly int _interruptStart;

    private readonly int _interruptEnd;

    private readonly int _statesPerScreenLine;

    private readonly int _expectedFrameCycles;
    
    public int FrameCycles => _frameCycles;
    
    public bool VRamUpdated { get; set; }

    public bool Fast { get; set; }
    
    public bool Slow { get; set; }
    
    public Worker(Model model, Interface @interface, VideoModulator videoModulator, AyAudio ayAudio)
    {
        _interface = @interface;

        _videoModulator = videoModulator;

        _ayAudio = ayAudio;

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;

        switch (model)
        {
            case Model.SpectrumPlus2A:
            case Model.SpectrumPlus3:
                _interruptStart = Constants.InterruptStartPlus;
                _interruptEnd = Constants.InterruptEndPlus;
                _statesPerScreenLine = Constants.StatesPerScreenLinePlus;
                _expectedFrameCycles = Constants.FrameCyclesPlus;

                break;
                
            default:
                _interruptStart = Constants.InterruptStart;
                _interruptEnd = Constants.InterruptEnd;
                _statesPerScreenLine = Constants.StatesPerScreenLine;
                _expectedFrameCycles = Constants.FrameCycles;
                    
                break;
        }
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

        // ReSharper disable once MethodSupportsCancellation
        _workerThread.Wait();

        _cancellationTokenSource.Dispose();
    }

    private void TimerWorker()
    {
        while (! _cancellationToken.IsCancellationRequested)
        {
            RunFrame();
        }
    }

    private void RunFrame()
    {
        try
        {
            if (! _paused)
            {
                _frameCycles = 0;

                while (_frameCycles < _expectedFrameCycles)
                {
                    if (_frameCycles >= _interruptStart && _frameCycles < _interruptEnd)
                    {
                        _interface.Int = true;

                        _scanStates = 0;

                        _lastScanComplete = 0;
                    }
                    else
                    {
                        _interface.Int = false;
                    }

                    VRamUpdated = false;

                    var cycles = OnTick(_frameCycles);

                    var instructionCycles = 0;

                    for (var i = 0; i < 7; i++)
                    {
                        if (i > 0 && cycles[i] == 0)
                        {
                            break;
                        }

                        _frameCycles += cycles[i];

                        instructionCycles += cycles[i];

                        _frameCycles += ApplyFrameRamChanges(i, _frameCycles, cycles);

                        if (cycles[i] > 0)
                        {
                            _videoModulator.CycleComplete(_frameCycles);
                        }
                    }

                    _scanStates += instructionCycles;

                    if (Slow && _scanStates - _lastScanComplete > _statesPerScreenLine * Constants.SlowScanFactor)
                    {
                        _scanResetEvent.WaitOne();

                        _lastScanComplete = _scanStates;
                    }
                }

                if (! Fast)
                {
                    _resetEvent.WaitOne();
                }

                Counters.Instance.IncrementCounter(Counter.SpectrumFrames);
            }

            _ayAudio.FrameReady(_resetEvent);

            _resetEvent.Reset();
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(Worker), exception);

            throw;
        }
    }

    private int ApplyFrameRamChanges(int mcycle, int frameCycles, byte[] opCycles)
    {
        if (mcycle < 6 && opCycles[mcycle + 1] == 0 && VRamUpdated)
        {
            return GetContention(frameCycles);
        }

        if (mcycle < 5 && opCycles[mcycle + 2] == 0 && VRamUpdated)
        {
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