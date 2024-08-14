using System.Runtime.InteropServices;
using Un4seen.Bass;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio;

public class AudioEngine : IDisposable
{
    private readonly int _sampleHandle;

    private static readonly AutoResetEvent ResetEvent = new(true);
    
    public AudioEngine()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && ! File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so")))
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X64:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so.x64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so"));

                    break;
                
                default:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so.arm64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so"));
                    
                    break;
            }
        }

        try
        {
            Bass.BASS_Init(-1, Constants.SampleRate, BASSInit.BASS_DEVICE_MONO, 0);
            
            _sampleHandle = Bass.BASS_SampleCreate(Constants.DefaultBufferSize, Constants.SampleRate, 1, 1, BASSFlag.BASS_SAMPLE_FLOAT);
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(AudioEngine), exception);
        }
    }

    public void Send(float[] data)
    {
        Bass.BASS_SampleSetData(_sampleHandle, data);
        
        var channel = Bass.BASS_SampleGetChannel(_sampleHandle, BASSFlag.BASS_SAMCHAN_STREAM);
        
        Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_END, 0, PlayComplete, IntPtr.Zero);
        
        Bass.BASS_ChannelPlay(channel, true);
        
        ResetEvent.WaitOne();
    }

    private static void PlayComplete(int handle, int channel, int data, IntPtr user)
    {
        ResetEvent.Set();
    }

    public void Dispose()
    {
        Bass.BASS_Free();
    }
}