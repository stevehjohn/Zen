using System.Runtime.InteropServices;
using Bufdio;
using Bufdio.Engines;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio;

public class AudioEngine : IDisposable
{
    private readonly IAudioEngine? _engine;

    public AudioEngine()
    {
        try
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    BufdioLib.InitializePortAudio();
                }
                else
                {
                    BufdioLib.InitializePortAudio(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libportaudio.dylib"));
                }
            }
            else
            {
                BufdioLib.InitializePortAudio(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libportaudio"));
            }

            _engine = new PortAudioEngine(new AudioEngineOptions(1, Constants.SampleRate));
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(AudioEngine), exception);
        }
    }

    public void Send(float[] data)
    {
        _engine?.Send(data);
    }

    public void Dispose()
    {
        _engine?.Dispose();
    }
}