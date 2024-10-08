﻿using Zen.Common;
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

    private int _lastScanComplete;

    private int _frameCycles;
    
    public int FrameCycles => _frameCycles;

    public bool Fast { get; set; }
    
    public bool Slow { get; set; }
    
    public bool Locked { get; set; }

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
            if (! Locked)
            {
                RunFrame();
            }
        }
    }

    public void RunFrame()
    {
        try
        {
            if (! _paused)
            {
                _frameCycles = 0;

                _videoModulator.StartFrame();

                while (_frameCycles < Constants.FrameCycles)
                {
                    if (_frameCycles is >= Constants.InterruptStart and < Constants.InterruptEnd)
                    {
                        _interface.INT = true;

                        _scanStates = 0;

                        _lastScanComplete = 0;
                    }
                    else
                    {
                        _interface.INT = false;
                    }

                    ClearFrameRamBuffer();

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

                    if (Slow && _scanStates - _lastScanComplete > Constants.StatesPerScreenLine * Constants.SlowScanFactor)
                    {
                        _scanResetEvent.WaitOne();

                        _lastScanComplete = _scanStates;
                    }
                }

                if (! Fast && ! Locked)
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