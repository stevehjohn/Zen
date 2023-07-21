using Bufdio;
using Bufdio.Engines;

namespace Zen.System.Modules;

public class Beeper : IDisposable
{
    private bool _bit3State;

    private bool _bit4State;

    private int _bufferPosition;

    private float _amplitude;

    private readonly IAudioEngine _engine;

    private readonly float[] _buffer;

    public Beeper()
    {
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio.dylib"));
        }
        else
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio"));
        }

        _engine = new PortAudioEngine(new AudioEngineOptions(1, Audio.Constants.SampleRate));

        _buffer = new float[Audio.Constants.SampleRate / 49];
    }

    public void UlaAddressed(byte value)
    {
        var amplitude = 0;

        var bit3State = (value & 0b0000_1000) > 0;

        if (_bit3State != bit3State)
        {
            amplitude = 1;

            _bit3State = bit3State;
        }

        var bit4State = (value & 0b0001_0000) > 0;

        if (_bit4State != bit4State)
        {
            amplitude = 2;

            _bit4State = bit4State;
        }

        _amplitude = (float) (amplitude * 0.5);
    }

    public void Sample()
    {
        _buffer[_bufferPosition] = _amplitude;

        _bufferPosition++;
    }

    public void PlayFrame()
    {
        _engine.Send(_buffer);

        Array.Fill(_buffer, _amplitude);

        _bufferPosition = 0;
    }

    public void Dispose()
    {
        _engine.Dispose();
    }
}