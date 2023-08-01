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

    private readonly VideoModulator _videoAdapter;

    private readonly AyAudio _ayAudio;

    private readonly Beeper _beeper;

    private readonly (int Address, byte Data)[] _vramChanges = new (int, byte)[2];

    private readonly ManualResetEvent _resetEvent = new(true);

    private bool _paused;

    private Task? _workerThread;

    public bool Fast { get; set; }

    public Worker(Interface @interface, VideoModulator videoAdapter, AyAudio ayAudio, Beeper beeper)
    {
        _interface = @interface;

        _videoAdapter = videoAdapter;

        _ayAudio = ayAudio;

        _beeper = beeper;

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

                    var sampleCycle = 0;

                    _videoAdapter.StartFrame();

                    while (frameCycles < Constants.FrameCycles)
                    {
                        _interface.INT = frameCycles is >= Constants.InterruptStart and < Constants.InterruptEnd;

                        ClearFrameRamBuffer();

                        var cycles = OnTick(frameCycles);

                        for (var i = 0; i < 7; i++)
                        {
                            if (i > 0 && cycles[i] == 0)
                            {
                                break;
                            }

                            sampleCycle += cycles[i];

                            if (sampleCycle > Beeper.BeeperTStateSampleRate && ! Fast)
                            {
                                sampleCycle -= Beeper.BeeperTStateSampleRate;

                                _beeper.Sample();
                            }

                            frameCycles += cycles[i];

                            frameCycles += ApplyFrameRamChanges(i, frameCycles, cycles);

                            if (cycles[i] > 0)
                            {
                                _videoAdapter.CycleComplete(frameCycles);
                            }
                        }
                    }

                    _resetEvent.WaitOne();

                    var queueResetEvent = _ayAudio.FrameReady(_resetEvent);

                    queueResetEvent.WaitOne();

                    queueResetEvent.Reset();

                    _resetEvent.Reset();

                    Counters.Instance.IncrementCounter(Counter.SpectrumFrames);
                }
            }
            catch (Exception exception)
            {
                File.AppendAllText("log.txt", exception.ToString());

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
            _videoAdapter.ApplyRamChange(_vramChanges[1].Address, _vramChanges[1].Data);

            return GetContention(frameCycles);
        }

        if (mcycle < 5 && opCycles[mcycle + 2] == 0 && _vramChanges[0].Address != -1)
        {
            _videoAdapter.ApplyRamChange(_vramChanges[0].Address, _vramChanges[0].Data);

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