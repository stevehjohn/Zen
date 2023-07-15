using Bufdio;
using Bufdio.Engines;
using Zen.System.Modules.Audio;

namespace Zen.System.Modules;

public class AyAudio : IDisposable
{
    private readonly List<Channel> _channels;

    private Task? _audioThread;

    private byte _registerNumber;

    private readonly float[] _buffer;

    private readonly IAudioEngine _engine;

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly byte[] _registerValues = new byte[256];

    public AyAudio()
    {
        _channels = new List<Channel>();

        for (var i = 0; i < Constants.Channels; i++)
        {
            _channels.Add(new Channel());
        }

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
                _channels[0].TonePeriod = (ushort) ((_channels[0].TonePeriod & 0x0F00) | value);
                break;

            case 1:
                _channels[0].TonePeriod = (ushort) ((_channels[0].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 2:
                _channels[1].TonePeriod = (ushort) ((_channels[1].TonePeriod & 0x0F00) | value);
                break;

            case 3:
                _channels[1].TonePeriod = (ushort) ((_channels[1].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 4:
                _channels[2].TonePeriod = (ushort) ((_channels[2].TonePeriod & 0x0F00) | value);
                break;

            case 5:
                _channels[2].TonePeriod = (ushort) ((_channels[2].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));

                _registerValues[_registerNumber] = (byte) (value & 0x0F);

                break;

            case 6:
                _channels[0].NoisePeriod = (byte) (value & 0x1F);
                _channels[1].NoisePeriod = (byte) (value & 0x1F);
                _channels[2].NoisePeriod = (byte) (value & 0x1F);

                _registerValues[_registerNumber] = (byte) (value & 0x1F);
                
                break;

            case 7:
                _channels[0].ToneOn = (value & 0b0000_0001) == 0;
                _channels[1].ToneOn = (value & 0b0000_0010) == 0;
                _channels[2].ToneOn = (value & 0b0000_0100) == 0;

                _channels[0].NoiseOn = (value & 0b0000_1000) == 0;
                _channels[1].NoiseOn = (value & 0b0001_0000) == 0;
                _channels[2].NoiseOn = (value & 0b0010_0000) == 0;

                break;

            case 8:
                if ((value & 0b0001_0000) > 0)
                {
                    _channels[0].EnvelopeOn = true;
                }
                else
                {
                    _channels[0].EnvelopeOn = false;
                    _channels[0].Volume = (byte) (value & 0x0F);
                }
                
                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 9:
                if ((value & 0b0001_0000) > 0)
                {
                    _channels[1].EnvelopeOn = true;
                }
                else
                {
                    _channels[1].EnvelopeOn = false;
                    _channels[1].Volume = (byte) (value & 0x0F);
                }
                
                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 10:
                if ((value & 0b0001_0000) > 0)
                {
                    _channels[2].EnvelopeOn = true;
                }
                else
                {
                    _channels[2].EnvelopeOn = false;
                    _channels[2].Volume = (byte) (value & 0x0F);
                }
                                
                _registerValues[_registerNumber] = (byte) (value & 0x1F);

                break;

            case 11:
                _channels[0].EnvelopePeriod = (ushort) ((_channels[0].EnvelopePeriod & 0xFF00) | value);
                _channels[1].EnvelopePeriod = (ushort) ((_channels[1].EnvelopePeriod & 0xFF00) | value);
                _channels[2].EnvelopePeriod = (ushort) ((_channels[2].EnvelopePeriod & 0xFF00) | value);
                break;

            case 12:
                _channels[0].EnvelopePeriod = (ushort) ((_channels[0].EnvelopePeriod & 0x00FF) | (value << 8));
                _channels[1].EnvelopePeriod = (ushort) ((_channels[1].EnvelopePeriod & 0x00FF) | (value << 8));
                _channels[2].EnvelopePeriod = (ushort) ((_channels[2].EnvelopePeriod & 0x00FF) | (value << 8));
                break;

            case 13:
                _channels[0].Envelope = (byte) (value & 0x0F);
                _channels[1].Envelope = (byte) (value & 0x0F);
                _channels[2].Envelope = (byte) (value & 0x0F);
                break;
        }
    }
    
    public void Dispose()
    {
        if (_audioThread != null)
        {
            _cancellationTokenSource.Cancel();

            try
            {
                // ReSharper disable once MethodSupportsCancellation
                _audioThread.Wait();
            }
            finally
            {
                _audioThread?.Dispose();
            }
            
            _cancellationTokenSource.Dispose();
        }

        _engine.Dispose();
    }

    private void RunFrame()
    {
        while (true)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                break;
            }

            for (var i = 0; i < Constants.BufferSize; i++)
            {
                var signal = _channels[0].GetNextSignal();

                signal += _channels[1].GetNextSignal();

                signal += _channels[2].GetNextSignal();

                _buffer[i] = signal;
            }

            _engine.Send(_buffer);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}