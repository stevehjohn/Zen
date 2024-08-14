using System.Runtime.InteropServices;
using Bufdio;
using Bufdio.Engines;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio.Engines;

public class PortAudioEngine : IZenAudioEngine
{
    private readonly IAudioEngine? _engine;

    public PortAudioEngine()
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
                    BufdioLib.InitializePortAudio(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libportaudio.dylib"));
                }
            }
            else
            {
                BufdioLib.InitializePortAudio(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libportaudio"));
            }

            _engine = new Bufdio.Engines.PortAudioEngine(new AudioEngineOptions(1, Constants.SampleRate));
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(PortAudioEngine), exception);
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