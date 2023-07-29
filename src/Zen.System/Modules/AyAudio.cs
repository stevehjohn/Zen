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

    private readonly DigitalToAudioConverter _digitalToAudioConverter = new();

    private Task? _audioThread;

    private byte _registerNumber;

    private readonly float[] _buffer;

    private readonly IAudioEngine _engine;

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly byte[] _registerValues = new byte[256];

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
    }

    public void Start()
    {
        _audioThread = Task.Run(RunFrame, _cancellationToken);
    }

    public void SelectRegister(byte registerNumber)
    {
        _registerNumber = registerNumber;
    }

    public byte GetRegister()
    {
        return _registerValues[_registerNumber];
    }

    public void SetRegister(byte value)
    {
        _registerValues[_registerNumber] = value;

        switch (_registerNumber)
        {
            case 0:
                _toneA.FinePeriod = value;

                break;

            case 1:
                _toneA.CoarsePeriod = value;

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 2:
                _toneB.FinePeriod = value;

                break;

            case 3:
                _toneB.CoarsePeriod = value;

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 4:
                _toneC.FinePeriod = value;

                break;

            case 5:
                _toneC.CoarsePeriod = value;

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 6:
                //_channels[0].NoisePeriod = (byte) (value & 0x1F);
                //_channels[1].NoisePeriod = (byte) (value & 0x1F);
                //_channels[2].NoisePeriod = (byte) (value & 0x1F);

                _registerValues[_registerNumber] = (byte) (value & 0x1F);
                
                break;

            case 7:
                //_channels[0].ToneOn = (value & 0b0000_0001) == 0;
                //_channels[1].ToneOn = (value & 0b0000_0010) == 0;
                //_channels[2].ToneOn = (value & 0b0000_0100) == 0;

                //_channels[0].NoiseOn = (value & 0b0000_1000) == 0;
                //_channels[1].NoiseOn = (value & 0b0001_0000) == 0;
                //_channels[2].NoiseOn = (value & 0b0010_0000) == 0;

                break;

            case 8:
                if ((value & 0b0001_0000) > 0)
                {
                    _digitalToAudioConverter.ChannelAEnvelopeOn = true;
                }
                else
                {
                    _digitalToAudioConverter.ChannelAEnvelopeOn = false;
                    _digitalToAudioConverter.ChannelAVolume = value;
                }

                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 9:
                if ((value & 0b0001_0000) > 0)
                {
                    _digitalToAudioConverter.ChannelBEnvelopeOn = true;
                }
                else
                {
                    _digitalToAudioConverter.ChannelBEnvelopeOn = false;
                    _digitalToAudioConverter.ChannelBVolume = value;
                }

                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 10:
                if ((value & 0b0001_0000) > 0)
                {
                    _digitalToAudioConverter.ChannelCEnvelopeOn = true;
                }
                else
                {
                    _digitalToAudioConverter.ChannelCEnvelopeOn = false;
                    _digitalToAudioConverter.ChannelCVolume = value;
                }

                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 11:
                _digitalToAudioConverter.EnvelopeGenerator.FinePeriod = value;
                
                break;

            case 12:
                _digitalToAudioConverter.EnvelopeGenerator.CoarsePeriod = value;
                
                break;

            case 13:
                //_channels[0].Envelope = (byte) (value & 0x0F);
                //_channels[1].Envelope = (byte) (value & 0x0F);
                //_channels[2].Envelope = (byte) (value & 0x0F);
                
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

        while (! _cancellationToken.IsCancellationRequested)
        {
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
                    _digitalToAudioConverter.GetChannelSignals(signals, _toneA.GetNextSignal(), _toneB.GetNextSignal(), _toneC.GetNextSignal());
                }

                SignalHook?.Invoke(signals);

                var signal = signals[0];

                signal += signals[1];

                signal += signals[2];

                _buffer[i] = signal;
            }

            _engine.Send(_buffer);

            Counters.Instance.IncrementCounter(Counter.AyFrames);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}