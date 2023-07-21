using System.Diagnostics;
using Bufdio;
using Bufdio.Engines;
using Zen.Common;

namespace Zen.System.Modules;

public class Beeper : IDisposable
{
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

        _buffer = new float[Constants.FrameCycles / Audio.Constants.BeeperTStateSampleRate];
    }

    public void UlaAddressed(byte value)
    {
        _amplitude = (value & 0b0001_0000) > 0 ? 1 : 0;
    }

    public void Sample()
    {
        _buffer[_bufferPosition] = _amplitude;

        _bufferPosition++;
    }

    public void PlayFrame()
    {
        _engine.Send(_buffer);

        _bufferPosition = 0;
    }

    public void Dispose()
    {
        _engine.Dispose();
    }
}