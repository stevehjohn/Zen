using Bufdio.Engines;
using Bufdio;
using Zen.System.Modules.Audio;

namespace Zen.System.Modules;

public class AyAudio : IDisposable
{
    private readonly List<Channel> _channels;

    private byte _mixer;

    private Task? _audioThread;

    private readonly float[] _buffer;

    private readonly IAudioEngine _engine;

    public AyAudio()
    {
        _channels = new List<Channel>();

        for (var i = 0; i < Constants.Channels; i++)
        {
            _channels.Add(new Channel());
        }

        _buffer = new float[Constants.BufferSize];

        BufdioLib.InitializePortAudio();

        _engine = new PortAudioEngine(new AudioEngineOptions(1, Constants.SampleRate));
    }

    public void Start()
    {
        _audioThread = Task.Run(RunFrame);
    }

    public void SetRegister(byte registerNumber, byte value)
    {
        switch (registerNumber)
        {
            case 0:
                _channels[0].TonePeriod = (ushort) ((_channels[0].TonePeriod & 0x0F00) | value);
                break;

            case 1:
                _channels[0].TonePeriod = (ushort) ((_channels[0].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));
                break;

            case 2:
                _channels[1].TonePeriod = (ushort) ((_channels[1].TonePeriod & 0x0F00) | value);
                break;

            case 3:
                _channels[1].TonePeriod = (ushort) ((_channels[1].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));
                break;

            case 4:
                _channels[2].TonePeriod = (ushort) ((_channels[2].TonePeriod & 0x0F00) | value);
                break;

            case 5:
                _channels[2].TonePeriod = (ushort) ((_channels[2].TonePeriod & 0x00FF) | ((value & 0x0F) << 8));
                break;

            case 6:
                _channels[0].NoisePeriod = (byte) (value & 0x1F);
                _channels[1].NoisePeriod = (byte) (value & 0x1F);
                _channels[2].NoisePeriod = (byte) (value & 0x1F);
                break;

            case 7:
                _mixer = value;
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

                break;

            case 11:
                _channels[0].EnvelopePeriod = (ushort) ((_channels[0].EnvelopePeriod & 0x0F00) | value);
                _channels[1].EnvelopePeriod = (ushort) ((_channels[1].EnvelopePeriod & 0x0F00) | value);
                _channels[2].EnvelopePeriod = (ushort) ((_channels[2].EnvelopePeriod & 0x0F00) | value);
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

    private void RunFrame()
    {
        while (true)
        {
            for (var i = 0; i < Constants.BufferSize; i++)
            {
                _buffer[i] = (_mixer & 0b0000_0001) > 0 ? _channels[0].GetNextToneSignal() : 0;
                _buffer[i] += (_mixer & 0b0000_1000) > 0 ? _channels[0].GetNextNoiseSignal() : 0;
                _buffer[i] += (_mixer & 0b0000_0010) > 0 ? _channels[1].GetNextToneSignal() : 0;
                _buffer[i] += (_mixer & 0b0001_0000) > 0 ? _channels[1].GetNextNoiseSignal() : 0;
                _buffer[i] += (_mixer & 0b0000_0100) > 0 ? _channels[2].GetNextToneSignal() : 0;
                _buffer[i] += (_mixer & 0b0010_0000) > 0 ? _channels[2].GetNextNoiseSignal() : 0;
            }

            _engine.Send(_buffer);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void Dispose()
    {
        _audioThread?.Dispose();

        _engine.Dispose();
    }
}