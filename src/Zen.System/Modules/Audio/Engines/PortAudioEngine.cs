using System.Runtime.InteropServices;
using Bufdio;
using Bufdio.Engines;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio.Engines;

public class PortAudioEngine : IZenAudioEngine
{
    private IAudioEngine? _engine;

    public PortAudioEngine()
    {
        Initialise();
    }

    public void Send(float[] data)
    {
        _engine?.Send(data);
    }

    public void Reset()
    {
        _engine?.Dispose();
        
        Initialise();
    }

    public void Dispose()
    {
        _engine?.Dispose();
    }

    private void Initialise()
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
}