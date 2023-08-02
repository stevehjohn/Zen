using Bufdio;
using Bufdio.Engines;

namespace Zen.System.Modules.Audio;

public class AudioEngine : IDisposable
{
    private readonly IAudioEngine _engine;

    public AudioEngine()
    {
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio.dylib"));
        }
        else
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio"));
        }

        _engine = new PortAudioEngine(new AudioEngineOptions(1, Constants.SampleRate));
    }

    public void Send(float[] data)
    {
        _engine.Send(data);
    }

    public void Dispose()
    {
        _engine.Dispose();
    }
}