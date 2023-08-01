using Bufdio;
using Bufdio.Engines;
using Zen.Common.Infrastructure;
using Zen.System.Modules.Audio;

namespace Zen.System.Modules;

public class AyAudio : IDisposable
{
    private readonly ToneGenerator _toneA = new();

    private readonly ToneGenerator _toneB = new();

    private readonly ToneGenerator _toneC = new();

    private readonly NoiseGenerator _noiseGenerator = new();

    private readonly MixerDac _mixerDac = new();

    private Task? _audioThread;

    private byte _registerNumber;

    private readonly float[] _buffer;

    private readonly IAudioEngine _engine;

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly byte[] _registerValues = new byte[256];

    private readonly ManualResetEvent _resetEvent = new(false);

    private readonly Queue<(int Frame, byte Port, byte Value)>[] _commandQueues;

    private int _readQueue;

    private ManualResetEvent? _workerResetEvent;

    public bool Silent { get; set; }

    public Action<float[]>? SignalHook { get; set; }

    public AyAudio()
    {
        _buffer = new float[Constants.BufferSize];

        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio.dylib"));
        }
        else
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio"));
        }

        _engine = new PortAudioEngine(new AudioEngineOptions(1, Constants.SampleRate));

        _cancellationTokenSource = new CancellationTokenSource();

        _cancellationToken = _cancellationTokenSource.Token;

        _commandQueues = new Queue<(int Frame, byte Port, byte Value)>[2];

        _commandQueues[0] = new Queue<(int Frame, byte Port, byte Value)>();

        _commandQueues[1] = new Queue<(int Frame, byte Port, byte Value)>();
    }

    public void Start()
    {
        _audioThread = Task.Run(RunFrame, _cancellationToken);
    }

    public void FrameReady(ManualResetEvent resetEvent)
    {
        _workerResetEvent = resetEvent;

        _resetEvent.Set();
    }

    public void SelectRegister(int cycle, byte registerNumber)
    {
        _commandQueues[1 - _readQueue].Enqueue((cycle, 0xC0, registerNumber));
    }

    public void SetRegister(int cycle, byte value)
    {
        _registerValues[_registerNumber] = value;

        switch (_registerNumber)
        {
            case 1:
            case 3:
            case 5:
            case 13:
                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 8:
            case 9:
            case 10:
                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;
        }

        _commandQueues[1 - _readQueue].Enqueue((cycle, 0x80, value));
    }

    public byte GetRegister()
    {
        return _registerValues[_registerNumber];
    }

    private void SelectRegisterInternal(byte registerNumber)
    {
        _registerNumber = registerNumber;
    }

    private void SetRegisterInternal(byte value)
    {
        switch (_registerNumber)
        {
            case 0:
                _toneA.FinePeriod = value;

                break;

            case 1:
                _toneA.CoarsePeriod = value;

                break;

            case 2:
                _toneB.FinePeriod = value;

                break;

            case 3:
                _toneB.CoarsePeriod = value;

                break;

            case 4:
                _toneC.FinePeriod = value;

                break;

            case 5:
                _toneC.CoarsePeriod = value;

                break;

            case 6:
                _noiseGenerator.Period = value;

                break;

            case 7:
                _mixerDac.ToneAOn = (value & 0b0000_0001) == 0;
                _mixerDac.ToneBOn = (value & 0b0000_0010) == 0;
                _mixerDac.ToneCOn = (value & 0b0000_0100) == 0;

                _mixerDac.NoiseAOn = (value & 0b0000_1000) == 0;
                _mixerDac.NoiseBOn = (value & 0b0001_0000) == 0;
                _mixerDac.NoiseCOn = (value & 0b0010_0000) == 0;

                break;

            case 8:
                if ((value & 0b0001_0000) > 0)
                {
                    _mixerDac.ChannelAEnvelopeOn = true;
                }
                else
                {
                    _mixerDac.ChannelAEnvelopeOn = false;
                    _mixerDac.ChannelAVolume = value;
                }

                break;

            case 9:
                if ((value & 0b0001_0000) > 0)
                {
                    _mixerDac.ChannelBEnvelopeOn = true;
                }
                else
                {
                    _mixerDac.ChannelBEnvelopeOn = false;
                    _mixerDac.ChannelBVolume = value;
                }

                break;

            case 10:
                if ((value & 0b0001_0000) > 0)
                {
                    _mixerDac.ChannelCEnvelopeOn = true;
                }
                else
                {
                    _mixerDac.ChannelCEnvelopeOn = false;
                    _mixerDac.ChannelCVolume = value;
                }

                break;

            case 11:
                _mixerDac.EnvelopeGenerator.FinePeriod = value;

                break;

            case 12:
                _mixerDac.EnvelopeGenerator.CoarsePeriod = value;

                break;

            case 13:
                _mixerDac.EnvelopeGenerator.Properties = value;

                break;
        }
    }

    public void Dispose()
    {
        if (_audioThread == null)
        {
            _cancellationTokenSource.Dispose();

            return;
        }

        _cancellationTokenSource.Cancel();

        _cancellationTokenSource.Dispose();

        _engine.Dispose();
    }

    private void RunFrame()
    {
        var signals = new float[3];

        var bufferStep = (float) Common.Constants.FrameCycles / (Constants.BufferSize - 1);

        while (! _cancellationToken.IsCancellationRequested)
        {
            try
            {
                _resetEvent.WaitOne();

                _resetEvent.Reset();

                for (var i = 0; i < Constants.BufferSize; i++)
                {
                    if (Silent)
                    {
                        signals[0] = 0;
                        signals[1] = 0;
                        signals[2] = 0;
                    }
                    else
                    {
                        var currentFrame = (int) Math.Ceiling(i * bufferStep);

                        while (_commandQueues[_readQueue].Count > 0 && _commandQueues[_readQueue].TryPeek(out var command))
                        {
                            if (command.Frame > currentFrame)
                            {
                                break;
                            }

                            _commandQueues[_readQueue].TryDequeue(out command);

                            if (command.Port == 0xC0)
                            {
                                SelectRegisterInternal(command.Value);
                            }
                            else
                            {
                                SetRegisterInternal(command.Value);
                            }
                        }

                        _mixerDac.GetChannelSignals(signals, _toneA.GetNextSignal(), _toneB.GetNextSignal(), _toneC.GetNextSignal(), _noiseGenerator.GetNextSignal());
                    }

                    SignalHook?.Invoke(signals);

                    var signal = signals[0];

                    signal += signals[1];

                    signal += signals[2];

                    _buffer[i] = signal;
                }

                _readQueue = 1 - _readQueue;

                _commandQueues[1 - _readQueue].Clear();

                _engine.Send(_buffer);

                _workerResetEvent?.Set();

                Counters.Instance.IncrementCounter(Counter.AyFrames);

            }
            catch (Exception exception)
            {
                File.AppendAllText("log.txt", exception.ToString());

                throw;
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}