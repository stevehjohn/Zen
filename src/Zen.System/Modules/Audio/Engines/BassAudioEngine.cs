using System.Runtime.InteropServices;
using Zen.Common.Infrastructure;
using Zen.System.Modules.Audio.Engines.Bass;

namespace Zen.System.Modules.Audio.Engines;

public class BassAudioEngine : IZenAudioEngine
{
    private static readonly AutoResetEvent ResetEvent = new(true);

    private int _sampleHandle = -1;

    private int _channel = -1;
    
    private bool _first = true;
    
    public BassAudioEngine()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && ! File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libbass.so")))
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X64:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libbass.so.x64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libbass.so"));

                    break;
                
                default:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libbass.so.arm64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries", "libbass.so"));
                    
                    break;
            }
        }
        
        Initialise();
    }

    public void Send(float[] data)
    {
        if (_sampleHandle == -1)
        {
            return;
        }
        
        if (! _first)
        {
            ResetEvent.WaitOne();
        }
        else
        {
            _first = false;
        }

        ManagedBass.BASS_SampleSetData(_sampleHandle, data);
        
        _channel = ManagedBass.BASS_SampleGetChannel(_sampleHandle, BassFlag.BASS_SAMCHAN_STREAM);
        
        Logger.LogException(ManagedBass.BASS_ChannelSetSync(_channel, BassSync.BASS_SYNC_END | BassSync.BASS_SYNC_ONETIME, Constants.DefaultBufferSize * 4, PlayComplete, IntPtr.Zero).ToString(), new Exception());
        
        ManagedBass.BASS_ChannelPlay(_channel, false);
    }

    public void Reset()
    {
        Initialise();
    }

    public void Dispose()
    {
        ManagedBass.BASS_Free();
    }

    private static void PlayComplete(int handle, int channel, int data, IntPtr user)
    {
        ResetEvent.Set();
    }

    private void Initialise()
    {
        try
        {
            if (! ManagedBass.BASS_Init(-1, Constants.SampleRate, BassInit.BASS_DEVICE_MONO, IntPtr.Zero))
            {
                throw new BassException("Error initialising BASS library.");
            }

            _sampleHandle = ManagedBass.BASS_SampleCreate(Constants.DefaultBufferSize * 4, Constants.SampleRate, 1, 1, BassFlag.BASS_SAMPLE_FLOAT);

            if (_sampleHandle == 0)
            {
                throw new BassException("Error creating BASS sample.");
            }
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(BassAudioEngine), exception);
        }
    }
}