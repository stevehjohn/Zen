using Bufdio;
using Bufdio.Engines;

namespace Zen.System.Modules;

public class Beeper : IDisposable
{
    private float _bitValue;

    private float _amplitude;

    private readonly IAudioEngine _engine;

    private readonly float[] _buffer;

    public bool Silent { get; set; }

    public Action<float>? SignalHook { get; set; }

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

        _buffer = new float[1];
    }

    public void UlaAddressed(byte value)
    {
        _bitValue = (value & 0b0001_0000) > 0 ? 1 : 0;
    }

    public void Sample()
    {
        if (! Silent)
        {
            _amplitude += (_bitValue - _amplitude) / 11;

            _buffer[0] = _amplitude;

            _engine.Send(_buffer);

            if (SignalHook != null)
            {
                SignalHook(_amplitude);
            }
        }
    }

    public void Dispose()
    {
        _engine.Dispose();
    }
}